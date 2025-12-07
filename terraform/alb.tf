resource "yandex_alb_target_group" "vitrina_tg" {
  name = "vitrina-target-group"
}

resource "yandex_alb_backend_group" "vitrina_backend" {
  name = "vitrina-backend-group"

  http_backend {
    name = "vitrina-http-backend"

    target_group_ids = [yandex_alb_target_group.vitrina_tg.id]

    healthcheck {
  timeout  = "1s"
  interval = "2s"

  http_healthcheck {
    path = "/"
  }
}

  }
}

resource "yandex_alb_load_balancer" "vitrina_alb" {
  name       = "vitrina-alb"
  folder_id  = var.folder_id
  network_id = yandex_vpc_network.vitrina_network.id

  allocation_policy {
    location {
      zone_id   = "ru-central1-a"
      subnet_id = yandex_vpc_subnet.public.id
    }
  }

  listener {
  name = "http-listener"

  endpoint {
    address {
      external_ipv4_address {}
    }
    ports = ["80"]
  }

  http {
    handler {
      http_router_id = yandex_alb_http_router.vitrina_router.id
    }
  }
}

}


resource "yandex_alb_http_router" "vitrina_router" {
  name = "vitrina-router"
}

