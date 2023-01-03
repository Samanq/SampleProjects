# TeamCity Sample

## Installing TeamCity on docker
1. After installing docker on your machine open powershell and run this command.
```powershell
docker run --name teamcity-server-instance \
-v team_city_data:/data/teamcity_server/datadir \
-v team_city_logs:/opt/teamcity/logs \
-p 8111:8111 \
jetbrains/teamcity-server
```