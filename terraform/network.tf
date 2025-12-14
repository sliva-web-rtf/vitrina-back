resource "yandex_vpc_network" "vitrina_network" {
  name = var.network_name
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
    description    = "HTTPS from internet"
    port           = 80
    v4_cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    protocol       = "ANY"
    v4_cidr_blocks = ["0.0.0.0/0"]
  }
}

resource "yandex_vpc_security_group" "vm_sg" {
  name       = "vm-sg"
  network_id = yandex_vpc_network.vitrina_network.id

  ingress {
    protocol          = "TCP"
    description       = "From ALB to app"
    port              = 5000
    security_group_id = yandex_vpc_security_group.alb_sg.id
  }

  egress {
    protocol       = "ANY"
    v4_cidr_blocks = ["0.0.0.0/0"]
  }
}
