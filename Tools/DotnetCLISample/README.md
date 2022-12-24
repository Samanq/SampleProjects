# Dotnet

## Adding Credential to Nuget.Config for Dotnet CLI
dotnet nuget add source <PACKAGE_SOURCE_PATH> [--name <SOURCE_NAME>] [--username <USER>]
    [--password <PASSWORD>] [--store-password-in-clear-text]
    [--valid-authentication-types <TYPES>] [--configfile <FILE>]

sample
```powershell
dotnet nuget add source https://someServer/myTeam -n name -u myUsername -p myPassword --store-password-in-clear-text
```
----

## Checking the version
```powershell
dotnet --version
```
---
## List of installed SDKs
```powershell
dotnet --list-sdks
```
---
