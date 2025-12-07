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
