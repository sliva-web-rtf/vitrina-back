locals {
  instance_names = [for i in range(var.vm_count) : "vitrina-vm-${i+1}"]
}

# Image: ubuntu-22.04 / or choose a suitable image id
data "yandex_compute_image" "ubuntu" {
  family = "ubuntu-2204-lts"
}

resource "yandex_compute_instance" "vitrina_vm" {
  name        = "vitrina-vm"
  platform_id = "standard-v1"
  zone        = var.vm_zone

  resources {
    cores  = 2
    memory = 2
  }

  boot_disk {
    initialize_params {
      image_id = "fd84mnbiarffhtfrhnog"
      size     = 20
    }
  }

  network_interface {
    subnet_id = yandex_vpc_subnet.public.id
    nat       = true
  }

  metadata = {
    user-data = file("${path.module}/vm_startup.sh")
  }
}
