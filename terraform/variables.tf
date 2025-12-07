variable "folder_id" {
  description = "b1gclhrbqvaio6i08suh"
  type        = string
}

variable "zone" {
  type    = string
  default = "ru-central1-a"
}

variable "network_name" {
  type    = string
  default = "vitrina-net"
}

variable "subnet_cidr" {
  type    = string
  default = "10.10.10.0/24"
}

variable "vm_count" {
  type    = number
  default = 2
}

variable "ssh_key" {
  type = string
  description = "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQCCsl3NILo8r+JnTIhW6f3wGeqIMDRQ+sOKjvwQIQ5IpJlrHWQwt/ThfN+94oXdlzmWN/UIc3vzLATvdrVWCtFtovBq+2EfV8CkEtJd7BDoQdyNLUWKUoPAeYHRoqLNGSWn28uCp8Ca3l40LooVZUHvucOgSstj7NCn1QTEIVbnw7haR2IY95lMOmVzH+XDWdGvQ/X32q8i13sKJk6bH4JDsaAk8/M/VTWexFIcyue7cludyIyP2/BXci3t7d6X1o+XDxB2s4eheTG+LQmqWTzITtziTsz1sRzB+kGHgeLsnBMG2bTsSmxsRwrV53ybithxVEgLzLHvScMIBw7IjKNN rsa-key-20251004"
}

variable "vm_zone" {
  type        = string
  description = "ru-central1-a"
  default     = "ru-central1-a"
}

variable "image_id" {
  type = string
  description = "fd84mnbiarffhtfrhnog"
}

variable "service_account_key_file" {
  type = string
  description = "F:/Study/Project/Sliva/vitrina-back/terraform/key.json"
}

variable "cloud_id" {
  type = string
  description = "b1g1qglub2qdq4p5ibol"
}

variable "token" {
  type        = string
  description = "IAM token for Yandex Cloud"
}
