resource "yandex_compute_instance_group" "vitrina_ig" {
  name               = "vitrina-ig"
  folder_id          = var.folder_id
  service_account_id = yandex_iam_service_account.vitrina_sa.id

  instance_template {
    platform_id = "standard-v3"

    resources {
      cores  = 2
      memory = 2
    }

    boot_disk {
      initialize_params {
        image_id = var.image_id
      }
    }

    network_interface {
      subnet_ids = [yandex_vpc_subnet.private.id]
      security_group_ids = [yandex_vpc_security_group.vm_sg.id]
    }

    metadata = {
      user-data = file("${path.module}/vm_startup.sh")
    }
  }

  scale_policy {
    fixed_scale {
      size = 2
    }
  }

  deploy_policy {
    max_unavailable = 1
    max_expansion   = 1
  }

  allocation_policy {
    zones = ["ru-central1-a"]
  }
}
