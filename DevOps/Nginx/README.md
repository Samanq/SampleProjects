# Nginx Sample

## Introduction
Nginx is a high-performance **web server** and **reverse proxy** server widely used to serve web content efficiently. This tutorial covers the installation and basic configuration of Nginx on Linux-based systems.

---

## Installation

### On Ubuntu

1. **Update the package index:**
   ```bash
   sudo apt update
   ```

2. **Install Nginx:**
   ```bash
   sudo apt install nginx -y
   ```

3. **Verify installation:**
   ```bash
   nginx -v
   ```

4. **Start and enable the service:**
   ```bash
   sudo systemctl start nginx
   sudo systemctl enable nginx
   ```

4. **Get the installation path:**
    ```bash
    whereis nginx
    ```
---

## Configuration

Nginx's primary configuration file is located at `/etc/nginx/nginx.conf`. Additionally, virtual host configurations are typically stored in `/etc/nginx/sites-available/` or `/etc/nginx/conf.d/`.

### Basic Configuration Steps

#### 1. **Editing the Main Configuration File**
Open the main configuration file:
```bash
sudo nano /etc/nginx/nginx.conf
```
Key sections to note:
- **worker_processes:** Defines the number of worker processes.
- **events:** Manages connection handling.
- **http:** Contains settings for handling HTTP traffic.

#### 2. **Setting Up a Server Block (Virtual Host)**
Create a new file for your site configuration in the `/etc/nginx/sites-available/` directory:
```bash
sudo nano /etc/nginx/sites-available/example.com
```
Add the following configuration:
```nginx
server {
    listen 80;
    server_name example.com www.example.com;

    root /var/www/example.com/html;
    index index.html index.htm;

    location / {
        try_files $uri $uri/ =404;
    }
}
```

Or for docker
```nginx
server {
    listen 80;
    server_name example.example.com;

    location / {
        proxy_pass http://127.0.0.1:8081;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }

    error_page 502 /502.html;
    location = /502.html {
        root /var/www/html;
        internal;
    }
}

```

#### 3. **Enable the Configuration**
Link the configuration file to the `sites-enabled` directory:
```bash
sudo ln -s /etc/nginx/sites-available/example.com /etc/nginx/sites-enabled/
```

#### 4. **Test the Configuration**
Before reloading Nginx, test the configuration for syntax errors:
```bash
sudo nginx -t
```

#### 5. **Reload Nginx**
If the test is successful, reload Nginx:
```bash
sudo systemctl reload nginx
```

### Advanced Configurations

#### Enabling HTTPS with SSL/TLS
1. **Install Certbot:**
   ```bash
   sudo apt install certbot python3-certbot-nginx -y
   ```

2. **Obtain an SSL certificate:**
   ```bash
   sudo certbot --nginx -d example.com -d www.example.com
   ```

3. **Verify HTTPS setup:**
   Access your site using `https://example.com`.

#### Load Balancing
Add the following to your server block:
```nginx
upstream backend {
    server backend1.example.com;
    server backend2.example.com;
}

server {
    listen 80;
    server_name example.com;

    location / {
        proxy_pass http://backend;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

---