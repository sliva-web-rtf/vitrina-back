resource "yandex_vpc_network" "vitrina_network" {
  name        = var.network_name
  description = "VPC for vitrina project"
}

resource "yandex_vpc_subnet" "public" {
  name           = "public-subnet"
  zone           = "ru-central1-a"
  network_id     = yandex_vpc_network.vitrina_network.id
  v4_cidr_blocks = ["10.0.1.0/24"]
}

resource "yandex_vpc_subnet" "private" {
  name           = "private-subnet"
  zone           = "ru-central1-a"
  network_id     = yandex_vpc_network.vitrina_network.id
  v4_cidr_blocks = ["10.0.2.0/24"]
}

resource "yandex_vpc_security_group" "alb_sg" {
  name       = "alb-sg"
  network_id = yandex_vpc_network.vitrina_network.id
  
  ingress {
    protocol       = "TCP"
    description    = "Yandex ALB health checks"
    v4_cidr_blocks = [
      "198.18.235.0/24",
      "198.18.248.0/24"
    ]
    from_port = 1
    to_port   = 65535
  }

  # Публичный HTTP
  ingress {
    protocol       = "TCP"
    port           = 80
    v4_cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    protocol       = "TCP"
    port           = 443
    v4_cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    protocol       = "ANY"
    v4_cidr_blocks = ["0.0.0.0/0"]
  }
}

# Security Group для VM
resource "yandex_vpc_security_group" "vm_sg" {
  name       = "vm-sg"
  network_id = yandex_vpc_network.vitrina_network.id

  # Трафик от ALB к приложению
  ingress {
    protocol          = "TCP"
    port              = 5000
    security_group_id = yandex_vpc_security_group.alb_sg.id
  }

  # Health checks к backend
  ingress {
    protocol          = "TCP"
    port              = 5000
    predefined_target = "loadbalancer_healthchecks"
  }

  egress {
    protocol       = "ANY"
    v4_cidr_blocks = ["0.0.0.0/0"]
  }
}
