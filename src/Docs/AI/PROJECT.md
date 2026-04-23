# PROJECT.md

## 🚀 Visão Geral

Loqim é uma plataforma SaaS que automatiza atendimento de empresas via IA integrada ao WhatsApp.

Objetivo:
- Eliminar menus engessados
- Permitir atendimento inteligente baseado em contexto
- Converter atendimento em vendas/agendamentos

---

## 🏗️ Arquitetura

Padrão:
- Clean Architecture + DDD

Camadas:

- Domain → regras de negócio e entidades
- Application → casos de uso
- Infrastructure → persistência e integrações
- API → entrada (controllers + DTOs)

---

## ⚙️ Stack

- .NET 8
- EF Core
- PostgreSQL (ou equivalente)
- Mensageria futura (RabbitMQ/Kafka)
- Integração com LLM (OpenAI inicialmente)

---

## 📊 Estado atual

- Estrutura inicial criada
- Entidades de catálogo iniciadas
- Persistência ainda acoplada (em evolução)

---

## 🎯 Objetivo atual

- Criar repositórios por agregado
- Desacoplar persistência dos controllers
- Implementar EF Core corretamente

---

## 🔮 Visão futura

- Multi-tenant robusto
- Histórico de conversas
- Motor de regras por empresa
- Integração com pagamentos/agendamentos
- IA customizada por cliente