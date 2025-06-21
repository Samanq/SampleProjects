# V2ray Server

## Server Configuration (Ubuntu)

Update and install the following libraries.

```bash
sudo apt update && sudo apt install -y curl unzip ntp
```

Ensure the time is sync

```bash
timedatectl status
```

Activate the NTP

```bash
sudo systemctl enable --now ntp
```

Navigate to your download folder, install and enable v2ray

```bash
curl -O https://raw.githubusercontent.com/v2fly/fhs-install-v2ray/master/install-release.sh
```

```bash
sudo bash install-release.sh
```

```bash
sudo systemctl enable --now v2ray
```

Run the following command to ensure that v2ray is active (running)

```bash
systemctl status v2ray
```

Generate a ```uuid``` and save it folder later configuration

```bash
uuidgen  
```

Generate a ```path``` and save it folder later configuration

```bash
openssl rand -hex 4  
```

Edit the v2ray config in ```/usr/local/etc/v2ray/config.json``` <br/>
Replace <your_generated_uuid> and <your_generated_path>

```bash
{
 "inbounds":[
   {
     "port":10000,
     "listen":"127.0.0.1",
     "protocol":"vmess",
     "settings":{"clients":[{"id":"<your_generated_uuid>","alterId":0}]},
     "streamSettings":{
       "network":"ws",
       "wsSettings":{"path":"/<your_generated_path>"}
     }
   }
 ],
 "outbounds":[{"protocol":"freedom","settings":{}}]
}
```

Restart the v2ray

```bash
sudo systemctl restart v2ray
```

Run the following command and ensure port `10000` is listening on `127.0.0.1`

```bash
sudo ss -lnpt | grep v2ray
```

> Tip: with the following command you can check the installation path of the v2ray

```bash
which v2ray
```

You can test the v2ray configuration with the following command, replace the installation path if it was needed

```bash
sudo /usr/local/bin/v2ray test -config=/usr/local/etc/v2ray/config.json
```


---

## Nginx configuration

Presuming the `nginx` and `certbot` is already installed.

Create a self-signed certificate

```bash
sudo mkdir -p /etc/ssl/private && cd /etc/ssl/private
openssl genrsa -out v2ray.key 2048
openssl req -new -key v2ray.key -out v2ray.csr \
  -subj "/CN=<your_server_IP>"
openssl x509 -req -days 365 -in v2ray.csr \
  -signkey v2ray.key -out v2ray.crt
```

With the following command test the certificate

```bash
openssl x509 -in v2ray.crt -noout -text
```

Open the `/etc/nginx/conf.d/v2ray.conf` and set the following configuration

```bash
server {
  listen 443 ssl;
  ssl_certificate     /etc/ssl/private/v2ray.crt;
  ssl_certificate_key /etc/ssl/private/v2ray.key;

  location = /<your_generated_path> {
    proxy_pass http://127.0.0.1:10000;
    proxy_http_version 1.1;
    proxy_set_header Upgrade    $http_upgrade;
    proxy_set_header Connection $connection_upgrade;
    proxy_set_header Host       $host;
    proxy_set_header X-Real-IP  $remote_addr;
    proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
    proxy_read_timeout 600s;
    proxy_redirect off;
  }
}
```

Open the `/etc/nginx/nginx.conf` and add map section in the `http` section, at the top.

```bash
map $http_upgrade $connection_upgrade {
    default   upgrade;
    ""        close;
}
```

Ensure the the `http` section ends these config

```bash
include /etc/nginx/conf.d/*.conf;
include /etc/nginx/sites-enabled/*;
```

Reload the nginx

```bash
sudo systemctl reload nginx
```

From your computer, not the server, run the following command to test the connection to the server

```bash
wscat --no-check -c wss://<your_server_ip>/<your_generated_path>
```

---

## Client Configuration

Replace <your_server_ip> with your server IP, copy this configuration and import into `NPV VPN` app as v2ray json config.

```json
{
  "log": {
    "loglevel": "warning",
    "access": "",
    "error": ""
  },
  "inbounds": [
    {
      "tag": "socks",
      "port": 10808,
      "protocol": "socks",
      "settings": {
        "auth": "noauth",
        "udp": true,
        "userLevel": 8
      }
    },
    {
      "tag": "http",
      "port": 10809,
      "protocol": "http",
      "settings": {
        "userLevel": 8
      }
    }
  ],
  "outbounds": [
    {
      "tag": "proxy",
      "protocol": "vmess",
      "settings": {
        "vnext": [
          {
            "address": "<your_server_ip>",
            "port": 443,
            "users": [
              {
                "id": "<your_generated_uuid>",
                "alterId": 0,
                "security": "auto",
                "level": 8
              }
            ]
          }
        ]
      },
      "streamSettings": {
        "network": "ws",
        "security": "tls",
        "wsSettings": {
          "path": "/<your_generated_path>",
          "headers": {
            "Host": "<your_server_ip>"
          }
        },
        "tlsSettings": {
          "allowInsecure": true,
          "serverName": "<your_server_ip>"
        }
      },
      "mux": {
        "enabled": false,
        "concurrency": -1
      }
    },
    {
      "tag": "direct",
      "protocol": "freedom",
      "settings": {}
    },
    {
      "tag": "block",
      "protocol": "blackhole",
      "settings": {
        "response": {
          "type": "http"
        }
      }
    }
  ],
  "routing": {
    "domainStrategy": "IPIfNonMatch",
    "rules": [
      {
        "type": "field",
        "inboundTag": [
          "api"
        ],
        "outboundTag": "api"
      },
      {
        "type": "field",
        "outboundTag": "proxy",
        "domain": [
          "geosite:google",
          "geosite:github",
          "geosite:netflix",
          "geosite:youtube",
          "geosite:facebook",
          "geosite:twitter"
        ]
      },
      {
        "type": "field",
        "outboundTag": "direct",
        "domain": [
          "geosite:cn"
        ]
      },
      {
        "type": "field",
        "outboundTag": "direct",
        "ip": [
          "geoip:private",
          "geoip:cn"
        ]
      },
      {
        "type": "field",
        "port": "0-65535",
        "outboundTag": "proxy"
      }
    ]
  },
  "dns": {
    "hosts": {
      "domain:googleapis.cn": "googleapis.com"
    },
    "servers": [
      {
        "address": "https+local://8.8.8.8/dns-query",
        "domains": [
          "geosite:google",
          "geosite:github",
          "geosite:netflix",
          "geosite:youtube"
        ]
      },
      {
        "address": "223.5.5.5",
        "port": 53,
        "domains": [
          "geosite:cn"
        ]
      },
      "8.8.8.8",
      "1.1.1.1"
    ]
  }
}
```
