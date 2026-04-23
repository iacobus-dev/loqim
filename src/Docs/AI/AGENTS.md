# AGENTS.md

## 📌 Projeto
Loqim é uma plataforma SaaS multi-tenant que utiliza IA para automatizar atendimento e operações via WhatsApp.

## 🧠 Stack
- .NET 8
- Clean Architecture
- DDD (Domain-Driven Design)
- EF Core
- PostgreSQL (ou compatível)
- Integração futura com mensageria (RabbitMQ/Kafka)

---

## 📂 Contexto obrigatório

Antes de qualquer alteração, considerar:

- /docs/AI/PROJECT.md
- /docs/AI/DOMAIN.md
- /docs/AI/ROADMAP.md

---

## 🧱 Princípios obrigatórios

- Respeitar Clean Architecture
- Separar claramente:
  - Domain
  - Application
  - Infrastructure
  - API

- NÃO:
  - Colocar regra de negócio em controllers
  - Acessar DbContext direto na API
  - Misturar domínio com infra

---

## 🧩 Domínio

- Trabalhar por agregados
- Cada agregado deve ser independente
- Regras de negócio ficam no domínio ou application

---

## 🔐 Multi-tenant

- Toda entidade deve possuir `TenantId`
- Toda query deve considerar TenantId
- Nunca expor dados entre tenants

---

## ⚙️ Repositórios

- Interface: Domain ou Application
- Implementação: Infrastructure
- Nunca acessar EF diretamente fora da Infrastructure

---

## 🧭 Modo de trabalho

- Sempre sugerir soluções escaláveis
- Preferir mudanças pequenas e seguras
- Explicar rapidamente antes de mudanças grandes
- Preservar consistência do projeto

---

## 🎯 Objetivo atual

Implementar repositórios por agregado e estruturar camada Infrastructure com EF Core.