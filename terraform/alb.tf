# Target Group (backend для ALB)
resource "yandex_alb_target_group" "vitrina_tg" {
  name = "vitrina-target-group"

  # ALB и ВМ должны быть в одной сети, но могут быть разных подсетей
  # Используем private IP ВМ
  target {
    subnet_id  = yandex_vpc_subnet.private.id
    ip_address = yandex_compute_instance_group.vitrina_ig.id
  }
}

# Backend Group с healthcheck
resource "yandex_alb_backend_group" "vitrina_backend" {
  name = "vitrina-backend-group"

  http_backend {
    name = "vitrina-http-backend"
    port = 5000   # порт приложения

    target_group_ids = [yandex_alb_target_group.vitrina_tg.id]

    healthcheck {
      timeout  = "2s"
      interval = "5s"

      http_healthcheck {
        path = "/swagger"
      }
    }
  }
}

# HTTP Router + Virtual Host + Route
resource "yandex_alb_http_router" "vitrina_router" {
  name = "vitrina-router"
}

resource "yandex_alb_virtual_host" "vitrina_vhost" {
  name           = "vitrina-vhost"
  http_router_id = yandex_alb_http_router.vitrina_router.id

  route {
    name = "all"

    http_route {
      http_route_action {
        backend_group_id = yandex_alb_backend_group.vitrina_backend.id
      }
    }
  }
}

# Load Balancer
resource "yandex_alb_load_balancer" "vitrina_alb" {
  name       = "vitrina-alb"
  folder_id  = var.folder_id
  network_id = yandex_vpc_network.vitrina_network.id

  security_group_ids = [yandex_vpc_security_group.alb_sg.id]
  
  allocation_policy {
    location {
      zone_id   = "ru-central1-a"
      subnet_id = yandex_vpc_subnet.public.id   # ALB в public subnet
    }
  }

  listener {
    name = "http-listener"

    endpoint {
      address {
        external_ipv4_address {}
      }
      ports = ["80"]        # публичный порт ALB
    }

    http {
      handler {
        http_router_id = yandex_alb_http_router.vitrina_router.id
      }
    }
  }
}
