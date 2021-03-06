{{/* vim: set filetype=mustache: */}}
{{/*
Expand the name of the chart.
*/}}
{{- define "azure-industrial-iot.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/}}
{{- define "azure-industrial-iot.fullname" -}}
{{- if .Values.fullnameOverride -}}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- $name := default .Chart.Name .Values.nameOverride -}}
{{- if contains $name .Release.Name -}}
{{- .Release.Name | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" -}}
{{- end -}}
{{- end -}}
{{- end -}}

{{/*
Create the name of the service account to use.
*/}}
{{- define "azure-industrial-iot.serviceAccountName" -}}
{{- if .Values.serviceAccount.create -}}
{{ default (include "azure-industrial-iot.fullname" .) .Values.serviceAccount.name }}
{{- else -}}
{{ default "default" .Values.serviceAccount.name }}
{{- end -}}
{{- end -}}

{{/*
Create the name of secret that will contain details of Azure resources.
*/}}
{{- define "azure-industrial-iot.env.fullname" -}}
{{- printf "%s-%s" .Release.Name "azure-industrial-iot-env" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Create the names of Industrial-IoT microservices.
*/}}
{{- define "azure-industrial-iot.registry.fullname" -}}
{{- printf "%s-%s" .Release.Name "registry" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "azure-industrial-iot.twin.fullname" -}}
{{- printf "%s-%s" .Release.Name "twin" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "azure-industrial-iot.history.fullname" -}}
{{- printf "%s-%s" .Release.Name "history" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "azure-industrial-iot.gateway.fullname" -}}
{{- printf "%s-%s" .Release.Name "gateway" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "azure-industrial-iot.vault.fullname" -}}
{{- printf "%s-%s" .Release.Name "vault" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "azure-industrial-iot.alerting.fullname" -}}
{{- printf "%s-%s" .Release.Name "alerting" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "azure-industrial-iot.onboarding.fullname" -}}
{{- printf "%s-%s" .Release.Name "onboarding" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "azure-industrial-iot.jobs.fullname" -}}
{{- printf "%s-%s" .Release.Name "jobs" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "azure-industrial-iot.model-processor.fullname" -}}
{{- printf "%s-%s" .Release.Name "model-processor" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "azure-industrial-iot.blob-notification.fullname" -}}
{{- printf "%s-%s" .Release.Name "blob-notification" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "azure-industrial-iot.ingress.fullname" -}}
{{- printf "%s-%s" .Release.Name "ingress" | trunc 63 | trimSuffix "-" -}}
{{- end -}}
