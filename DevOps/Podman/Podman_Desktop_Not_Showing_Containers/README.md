# Podman Desktop Not Showing Containers (Windows / WSL)

## Problem

Podman Desktop shows **no containers or images**, but the terminal does:

```powershell
podman ps --all
```

In **Settings > Troubleshooting**, **Ping** and **Check containers** fail with:

```
Error: connect ENOENT \\.\pipe\podman-machine-default
```

---

## Cause

The terminal and Podman Desktop reach the Podman machine in different ways:

| Client | Connects through |
|---|---|
| `podman` CLI | SSH (`ssh://user@127.0.0.1:<port>/...`) |
| Podman Desktop | Windows named pipe `\\.\pipe\podman-machine-default` |

The named pipe is created by a helper, `win-sshproxy.exe`, when the machine starts. The helper also creates a Unix socket at:

```
%TEMP%\podman\podman-machine-default-api.sock
```

A **stale socket file** was left in that location from an earlier run. The helper could not recreate it, so it exited at startup and the pipe was never created. The CLI kept working over SSH, which hid the problem.

`podman machine start` showed the warning:

```
API forwarding for Docker API clients is not available due to the following startup failures.
        win-sshproxy.exe failed to start, exit code: 0 (see windows event logs)
```

---

## Fix

1. **Stop the machine:**
   ```powershell
   podman machine stop
   ```

2. **Stop any leftover helper process:**
   ```powershell
   Stop-Process -Name win-sshproxy -ErrorAction SilentlyContinue
   ```

3. **Delete the stale socket file:**
   ```powershell
   Remove-Item "$env:TEMP\podman\podman-machine-default-api.sock"
   ```

4. **Start the machine:**
   ```powershell
   podman machine start
   ```
   The output should now include this line, without the warning:
   ```
   API forwarding listening on: npipe:////./pipe/docker_engine
   ```

5. **Refresh Podman Desktop:** go to **Settings > Troubleshooting** and click **Reconnect providers** if the containers don't appear on their own.

> ⚠️ Do **not** use **Clear / Purge Data** in Troubleshooting. It deletes the machine with all its containers, images and volumes.

### Quick Fix Script

[`fix-podman-pipe.ps1`](fix-podman-pipe.ps1) runs steps 1 to 4 and checks that the pipe was created. Run it from this folder in PowerShell:

```powershell
powershell -ExecutionPolicy Bypass -File .\fix-podman-pipe.ps1
```

For a machine with a different name:

```powershell
powershell -ExecutionPolicy Bypass -File .\fix-podman-pipe.ps1 -Machine my-other-machine
```

---

## Diagnosing It Again

Use these if the problem returns.

1. **Check the start output** for the `win-sshproxy.exe failed to start` warning:
   ```powershell
   podman machine stop; podman machine start
   ```

2. **Check whether the pipe exists:**
   ```powershell
   Get-ChildItem \\.\pipe\ | Where-Object Name -match 'podman|docker'
   ```

3. **Check whether the helper is running:**
   ```powershell
   tasklist /fi "imagename eq win-sshproxy.exe"
   ```

4. **Check for the stale socket file:**
   ```powershell
   Test-Path "$env:TEMP\podman\podman-machine-default-api.sock"
   ```

5. **See the exact command Podman uses to launch the helper:**
   ```powershell
   podman --log-level=debug machine start 2>&1 | Select-String sshproxy
   ```

---

## Notes

- **Rootless vs. rootful:** the machine has two connections, `podman-machine-default` (rootless, the default) and `podman-machine-default-root`. They hold **separate** containers. Podman Desktop shows only the rootless side, so containers created with the root connection won't appear there:
  ```powershell
  podman system connection list
  podman --connection podman-machine-default-root ps --all
  ```

- **Version mismatch:** the client was `6.1.4` while the machine (server) was still `6.0.2`. See [Upgrading the Machine on WSL](#upgrading-the-machine-on-wsl) to match them.

---

## Upgrading the Machine on WSL

`podman machine os upgrade` is **not supported** on WSL machines. The WSL machine is a Fedora system, so Podman inside it is upgraded with `dnf`. This keeps all containers, images and volumes.

> Always update **Podman for Windows first** (Podman Desktop or the installer), then upgrade the machine to the **same version**. Avoid a plain `dnf upgrade`: the repository also offers release candidates (e.g. `6.2.0~rc1`), which would put the machine ahead of the client.

1. **Check the client version:**
   ```powershell
   podman version
   ```

2. **List the versions available inside the machine:**
   ```powershell
   podman machine ssh "dnf --refresh list --upgrades 'podman*'"
   ```

3. **Upgrade to the client's exact version** (replace `6.1.4` with yours):
   ```powershell
   podman machine ssh "sudo dnf upgrade -y podman-6.1.4"
   ```

4. **Restart the machine and confirm both versions match:**
   ```powershell
   podman machine stop; podman machine start; podman version
   ```
   `Client` and `Server` should show the same `Version`.

### Disabling a Dead Repository

If `dnf` prints `Status code: 404` errors, the machine image may include a repository for an old pre-release build. On this machine it was in `podman-release-copr.repo`.

1. **List the repository files:**
   ```powershell
   podman machine ssh "ls /etc/yum.repos.d/"
   ```

2. **Check the file's contents** before changing it:
   ```powershell
   podman machine ssh "cat /etc/yum.repos.d/podman-release-copr.repo"
   ```

3. **Disable it** (keeps the file, so it can be re-enabled):
   ```powershell
   podman machine ssh "sudo sed -i 's/^enabled=1/enabled=0/' /etc/yum.repos.d/podman-release-copr.repo"
   ```
   Only do this when the file contains just the dead repository, since it disables every section in the file.
