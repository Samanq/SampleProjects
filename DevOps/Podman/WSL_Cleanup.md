# Podman WSL Disk Cleanup

## Introduction

On Windows, the Podman machine stores its whole Linux filesystem in a single virtual disk file:

```
C:\Users\saman\.local\share\containers\podman\machine\wsl\wsldist\podman-machine-default\ext4.vhdx
```

This file **grows** as images, containers and volumes are written, but **never shrinks** on its own, even after deleting them. Repeat this cleanup every few months to reclaim the space.

> Last run: the file went from **16.53 GB** to **7.51 GB**.

---

## 1. Check the Current Size

1. **Size of the disk file on Windows:**
   ```powershell
   "{0:N2} GB" -f ((Get-Item "$env:USERPROFILE\.local\share\containers\podman\machine\wsl\wsldist\podman-machine-default\ext4.vhdx").Length/1GB)
   ```

2. **Space actually used inside the machine:**
   ```powershell
   podman machine ssh "df -h /"
   ```
   If `Used` is much smaller than the file size, compacting will help. (The large `Size` value is just WSL's maximum and uses no real space.)

---

## 2. Remove Unused Podman Data

1. **See what can be reclaimed:**
   ```powershell
   podman system df
   ```

2. **List unused volumes** and check that none hold data you need:
   ```powershell
   podman volume ls --filter dangling=true
   ```
   Named volumes (e.g. `projecttracker.apphost-...-postgres-data`) can contain database data. Unnamed hash volumes are usually throwaway.

3. **Remove unused anonymous volumes:**
   ```powershell
   podman volume prune -f
   ```
   `prune` skips **named** volumes. Remove those by name if you no longer need them:
   ```powershell
   podman volume rm <volume-name> <volume-name>
   ```

4. **Remove unused images** (optional, only images not used by any container):
   ```powershell
   podman image prune -a -f
   ```

5. **Check the root connection too.** It holds separate containers, images and volumes:
   ```powershell
   podman --connection podman-machine-default-root system df
   ```

---

## 3. Trim the Filesystem

Mark all free blocks as unused so Windows can drop them. Safe to run while the machine is running.

```powershell
podman machine ssh "sudo fstrim -av"
```

---

## 4. Stop the Machine and WSL

```powershell
podman machine stop; wsl --shutdown
```

> ⚠️ `wsl --shutdown` stops **all** WSL distributions. Save any work in other distributions first. Quit Podman Desktop as well, so it doesn't restart the machine.

---

## 5. Compact the Disk File

Open **PowerShell as Administrator** and run:

```powershell
@"
select vdisk file="C:\Users\saman\.local\share\containers\podman\machine\wsl\wsldist\podman-machine-default\ext4.vhdx"
attach vdisk readonly
compact vdisk
detach vdisk
"@ | diskpart
```

It should end with `DiskPart successfully compacted the virtual disk file.` If it says the file is in use, run `wsl --shutdown` again and retry.

---

## 6. Verify

1. **Check the new size** (normal PowerShell):
   ```powershell
   "{0:N2} GB" -f ((Get-Item "$env:USERPROFILE\.local\share\containers\podman\machine\wsl\wsldist\podman-machine-default\ext4.vhdx").Length/1GB)
   ```

2. **Start the machine and check the containers are still there:**
   ```powershell
   podman machine start; podman ps --all
   ```

3. **Update the "Last run" line** at the top of this file with the before and after sizes.
