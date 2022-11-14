# Serilog

## Required Packages
Install these packages
- Serilog.AspNetCore
- Serilog.Enrichers.Environment (If you need to get machine name)
- Serilog.Enrichers.Thread (If you need to get Thread)
- Serilog.Enrichers.Process (If you need to get Process)
- Serilog.Sinks.Seq (If you need to use seq)
---
## Runnig Seq in docker
```poweshell
docker run -d --restart unless-stopped --name seq -e ACCEPT_EULA=Y -v D:\Demos\Logs:/data -p 8887:80 datalust/seq:latest
```
After runnig the Seq you can browse it from http://localhost:8887