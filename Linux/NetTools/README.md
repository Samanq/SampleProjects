# NetTools Sample

## Installation
```bash
sudo apt update && sudo apt install net-tools
```

## Netstat Commands
| Command | Example | Description |
| --- | --- | --- |
| netstat | netstat -nulpt | Displays the list of listening ports |
| ss | ss -nulpt | Displays the list of listening ports |

## A Quick Modern Alternative: ss
Since you are working directly in the terminal managing network ports, it is worth noting that netstat is actually considered deprecated on modern Linux systems.

The standard replacement is ss (Socket Statistics), which is faster, shows more detailed information, and is usually pre-installed on almost all modern distributions as part of the iproute2 package.