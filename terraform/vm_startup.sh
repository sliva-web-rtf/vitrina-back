#!/bin/bash
set -e

# пример для Ubuntu 22.04
export DEBIAN_FRONTEND=noninteractive
apt-get update -y
apt-get install -y git wget apt-transport-https ca-certificates

# Установка .NET 8 (пример, проверь актуальность версии в доках Microsoft)
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
dpkg -i packages-microsoft-prod.deb
apt-get update -y
apt-get install -y dotnet-sdk-8.0

# Создадим папку и пользователя
useradd -m -s /bin/bash appuser || true
mkdir -p /srv/vitrina
chown appuser:appuser /srv/vitrina

# Клонируем репозиторий (замени, если нужен другой branch)
sudo -u appuser git clone https://github.com/sliva-web-rtf/vitrina-back.git /srv/vitrina || (cd /srv/vitrina && sudo -u appuser git pull)

# Сборка
cd /srv/vitrina
sudo -u appuser dotnet publish -c Release -o /srv/vitrina/published

# systemd сервис
cat >/etc/systemd/system/vitrina.service <<'EOF'
[Unit]
Description=Vitrina ASP.NET app
After=network.target

[Service]
User=appuser
WorkingDirectory=/srv/vitrina/published
ExecStart=/usr/bin/dotnet /srv/vitrina/published/Vitrina.Back.dll
Restart=always
RestartSec=5
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://0.0.0.0:5000

[Install]
WantedBy=multi-user.target
EOF

systemctl daemon-reload
systemctl enable --now vitrina.service

# (опционально) установка и настройка fail2ban / ufw
apt-get install -y ufw
ufw allow ssh
ufw allow 5000/tcp
ufw --force enable
