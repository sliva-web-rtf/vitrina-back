output "alb_public_ipv4" {
  value = yandex_alb_load_balancer.vitrina_alb.listener[0].endpoint[0].address[0].external_ipv4_address
}

output "instance_private_ips" {
  value = yandex_compute_instance.vitrina_vm.network_interface[0].ip_address
}
