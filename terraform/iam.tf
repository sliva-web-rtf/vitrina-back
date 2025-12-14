resource "yandex_iam_service_account" "vitrina_sa" {
  name = "vitrina-sa"
}

resource "yandex_resourcemanager_folder_iam_member" "sa-compute" {
  folder_id = var.folder_id
  role      = "compute.editor"
  member    = "serviceAccount:${yandex_iam_service_account.vitrina_sa.id}"
}

resource "yandex_resourcemanager_folder_iam_member" "sa-vpc" {
  folder_id = var.folder_id
  role      = "vpc.user"
  member    = "serviceAccount:${yandex_iam_service_account.vitrina_sa.id}"
}

resource "yandex_resourcemanager_folder_iam_member" "sa-editor" {
  folder_id = var.folder_id
  role      = "editor"
  member    = "serviceAccount:${yandex_iam_service_account.vitrina_sa.id}"
}
