# PostgreSQL Sample

Running the Postgres image on docker or podman

Podman

```bash
sudo podman run -d \
  --name postgres-db \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=Pass@12345 \
  -e POSTGRES_DB=default_database \
  -v postgres-data:/var/lib/postgresql/data \
  -p 5432:5432 \
  --restart=always \
  postgres
```

---

Docker

```bash
sudo docker run -d \
  --name postgres-db \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=Pass@12345 \
  -e POSTGRES_DB=default_database \
  -v postgres-data:/var/lib/postgresql/data \
  -p 5432:5432 \
  --restart=always \
  postgres
```

> Use ``` ` ``` instead of ```\\``` if you are using this command in powershell
