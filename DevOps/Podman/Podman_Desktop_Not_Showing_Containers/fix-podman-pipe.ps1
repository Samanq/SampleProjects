<#
.SYNOPSIS
    Fixes Podman Desktop not seeing containers on Windows (WSL machine).

.DESCRIPTION
    Removes the stale API socket that prevents win-sshproxy.exe from starting,
    restarts the Podman machine and checks that the named pipe Podman Desktop
    uses (\\.\pipe\<machine>) exists again.

    Containers, images and volumes are not touched.

.EXAMPLE
    .\fix-podman-pipe.ps1

.EXAMPLE
    .\fix-podman-pipe.ps1 -Machine my-other-machine
#>
param(
    [string]$Machine = "podman-machine-default"
)

$socket = Join-Path $env:TEMP "podman\$Machine-api.sock"

Write-Host "[1/4] Stopping machine '$Machine'..." -ForegroundColor Cyan
podman machine stop $Machine 2>&1 | Out-Host

Write-Host "[2/4] Stopping leftover win-sshproxy processes..." -ForegroundColor Cyan
$procs = Get-Process -Name win-sshproxy -ErrorAction SilentlyContinue
if ($procs) {
    $procs | Stop-Process -Force
    Write-Host "      Stopped $($procs.Count) process(es)."
} else {
    Write-Host "      None running."
}

Write-Host "[3/4] Removing stale socket..." -ForegroundColor Cyan
if (Test-Path $socket) {
    Remove-Item $socket -Force
    Write-Host "      Removed $socket"
} else {
    Write-Host "      No stale socket found."
}

Write-Host "[4/4] Starting machine '$Machine'..." -ForegroundColor Cyan
$output = podman machine start $Machine 2>&1 | Out-String
Write-Host $output

if ($LASTEXITCODE -ne 0) {
    Write-Host "FAILED: the machine did not start. See the output above." -ForegroundColor Red
    exit 1
}

if ($output -match "win-sshproxy.exe failed") {
    Write-Host "FAILED: win-sshproxy.exe still failed to start." -ForegroundColor Red
    Write-Host "Run the 'Diagnosing It Again' steps in README.md."
    exit 1
}

$pipe = Get-ChildItem \\.\pipe\ | Where-Object Name -eq $Machine
if ($pipe) {
    Write-Host "OK: \\.\pipe\$Machine exists." -ForegroundColor Green
    Write-Host "If Podman Desktop still shows nothing, click 'Reconnect providers' in Settings > Troubleshooting."
} else {
    Write-Host "FAILED: \\.\pipe\$Machine was not created." -ForegroundColor Red
    exit 1
}
