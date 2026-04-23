# ROADMAP.md

## 🎯 Objetivo atual

Estruturar backend corretamente com Clean Architecture e DDD.

---

## ✅ Já feito

- Estrutura inicial do projeto
- Criação das entidades:
  - CatalogServiceItem
  - CatalogProduct

---

## 🔧 Em andamento

- Separação de responsabilidades
- Remoção de lógica de persistência dos controllers

---

## 📌 Próximos passos (curto prazo)

### 1. Repositórios
- [ ] Criar interfaces de repositório por agregado
- [ ] Definir contratos no domínio/application

### 2. Infrastructure
- [ ] Implementar repositórios com EF Core
- [ ] Criar DbContext
- [ ] Mapear entidades

### 3. Banco de dados
- [ ] Criar migrations
- [ ] Configurar conexão

### 4. API
- [ ] Refatorar controllers para usar Application
- [ ] Remover acesso direto ao banco

---

## 🚀 Próximos passos (médio prazo)

- [ ] Multi-tenant robusto
- [ ] Sistema de regras por empresa
- [ ] Integração com WhatsApp provider
- [ ] Histórico de conversas

---

## 🧠 Próximos passos (avançado)

- [ ] IA customizada por cliente
- [ ] Engine de decisões
- [ ] Orquestração de fluxos
- [ ] Integração com pagamentos/agendamentos

---

## ⚠️ Regras de execução

- Não pular etapas
- Garantir consistência arquitetural
- Evitar atalhos que gerem dívida técnica