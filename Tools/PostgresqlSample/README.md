# PostgreSQL Sample

Running the Postgres image on docker
```powershell
docker run -d \
  --name my-postgres \
  -e POSTGRES_USER=your_username \
  -e POSTGRES_PASSWORD=your_password \
  -e POSTGRES_DB=your_database \
  -v C:\Volumes\Postgresql:/var/lib/postgresql/data \
  -p 5432:5432 \
  postgres
```
> Use "`" instead of "\\" if you are using this command in powershell
