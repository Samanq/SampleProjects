# n8n

n8n is an open-source workflow automation tool that enables users to connect various apps and services to automate tasks and data flows without extensive coding. It supports a wide range of integrations and provides a visual interface for building complex workflows.

## Installation

Run one of the following commands to run the n8n locally

```bash
podman run -d \
  --name n8n \
  -p 5678:5678 \
  -v n8n_data:/home/node/.n8n \
  docker.n8n.io/n8nio/n8n
```

```powershell
podman run -d `
  --name n8n `
  -p 5678:5678 `
  -v n8n_data:/home/node/.n8n `
  docker.n8n.io/n8nio/n8n
```

## Nginx Configuration

If you're deploying it on a nginx.

- First install a certificate using ```certbot``` [Nginx Guide](https://github.com/Samanq/SampleProjects/tree/main/DevOps/Nginx)

- Align your config file in the ```/etc/nginx/sites-available``` with the following configuration.

```bash
server {
    server_name n8n.example.com;

    location / {
        proxy_pass http://127.0.0.1:5678;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection "Upgrade";
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;

        # Timeout settings
        proxy_read_timeout 3600s;
        proxy_send_timeout 3600s;
    }

    error_page 502 /502.html;
    location = /502.html {
        root /var/www/html;
        internal;
    }


    listen 443 ssl; # managed by Certbot
    listen [::]:443 ssl;
    ssl_certificate /certpath.pem; # managed by Certbot
    ssl_certificate_key /certpath/privkey.pem; # managed by Certbot
    include /etc/letsencrypt/options-ssl-nginx.conf; # managed by Certbot
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem; # managed by Certbot

}server {
    if ($host = n8n.example.com) {
        return 301 https://$host$request_uri;
    } # managed by Certbot


    server_name n8n.example.com;
    listen 80;
    return 404; # managed by Certbot


}
```
