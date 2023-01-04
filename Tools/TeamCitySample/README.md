# TeamCity Sample
- You can have 3 build agents.
---
## Installing TeamCity on docker
1. After installing docker on your machine open powershell and run this command.
```powershell
docker run --name teamcity-server-instance -v team_city_data:/data/teamcity_server/datadir -v team_city_logs:/opt/teamcity/logs -p 8111:8111 jetbrains/teamcity-server
```
2. After the container stated you can browse it with http://localhost:8111
3. Click proceed.
4. For the Database type select Internal (HSQLDB) and click proceed.
5. Accept the license agreement and click continue.
6. Choose a username and password and login into the TeamCity.
---
