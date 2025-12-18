# ===== PostgreSQL =====
resource "yandex_mdb_postgresql_cluster" "project-postgres" {
  name                = "project-postgres"
  environment         = "PRODUCTION"
  network_id          = yandex_vpc_network.vitrina_network.id
  deletion_protection = false

  config {
    version = "15"
    resources {
      resource_preset_id = "s3-c2-m8"
      disk_type_id       = "network-ssd"
      disk_size          = 10
    }
  }

  host {
    zone             = yandex_vpc_subnet.private.zone
    name             = "postgresql-host"

    subnet_id        = yandex_vpc_subnet.private.id
    assign_public_ip = false
  }
}


resource "yandex_mdb_postgresql_database" "vitrina_test" {
  cluster_id = yandex_mdb_postgresql_cluster.project-postgres.id
  name       = "vitrina_test"
  owner      = yandex_mdb_postgresql_user.postgresql.name
}

resource "yandex_mdb_postgresql_user" "postgresql" {
  cluster_id = yandex_mdb_postgresql_cluster.project-postgres.id
  name       = "redman"
  password   = "password"
}

# ===== Docker build =====
#resource "null_resource" "docker-build-backend" {
# provisioner "local-exec" {
#   command = "docker build .. -t cr.yandex/${yandex_container_registry.coderev_registry.id}/backend:latest && docker push cr.yandex/${yandex_container_registry.coderev_registry.id}/backend:latest"
# }
#}
#resource "null_resource" "docker-build-frontend" {
# provisioner "local-exec" {
#   command = "docker build ../client -t cr.yandex/${yandex_container_registry.coderev_registry.id}/frontend:latest && docker push cr.yandex/${yandex_container_registry.coderev_registry.id}/frontend:latest"
# }
#}
# ===== Docker push =====
#resource "null_resource" "docker-push-backend" {
# provisioner "local-exec" {
#   command = ""
# }
#}
#resource "null_resource" "docker-push-frontend" {
# provisioner "local-exec" {
#   command = ""
# }
#}


# ===== Kubernetes pods =====
#resource "null_resource" "kubectl-config" {
# provisioner "local-exec" {
#   command = "yc managed-kubernetes cluster get-credentials --id ${yandex_kubernetes_cluster.zonal_cluster.id} --external"
# }
#}
#resource "null_resource" "deploy-app" {
#  provisioner "local-exec" {
#    command = "kubectl config unset clusters && kubectl config unset users && kubectl config unset contexts && yc managed-kubernetes cluster get-credentials --id ${yandex_kubernetes_cluster.zonal_cluster.id} --external && kubectl create namespace coderev && export POSTGRESQL_HOST=${yandex_mdb_postgresql_cluster.project-postgres.host[0].fqdn}; docker build --build-arg POSTGRESQL_HOST=$POSTGRESQL_HOST .. -t cr.yandex/${yandex_container_registry.coderev_registry.id}/backend:latest && docker push cr.yandex/${yandex_container_registry.coderev_registry.id}/backend:latest && docker build ../client -t cr.yandex/${yandex_container_registry.coderev_registry.id}/frontend:latest && docker push cr.yandex/${yandex_container_registry.coderev_registry.id}/frontend:latest && export CLUSTER_HOST=${yandex_container_registry.coderev_registry.id}; envsubst < ../ci/coderev.yaml | kubectl apply -f - && kubectl apply -f ../ci/load-balancer.yaml && kubectl apply -f ../ci/config-map.yaml && kubectl apply -f ../ci/config-map.yaml"
#  }
#}
#resource "null_resource" "coderev-pod" {
# provisioner "local-exec" {
#   command = "export CLUSTER_HOST=${yandex_container_registry.coderev_registry.id}; envsubst < ../ci/coderev.yaml | kubectl apply -f - && kubectl apply -f ../ci/load-balancer.yaml && kubectl apply -f ../ci/config-map.yaml && kubectl apply -f ../ci/config-map.yaml"
# }
#}
#resource "null_resource" "load-balancer-pod" {
# provisioner "local-exec" {
#   command = ""
# }
#}
#resource "null_resource" "configmap-pod" {
# provisioner "local-exec" {
#   command = ""
# }
#}