# DOMAIN.md

## 🧠 Conceito

O domínio do Loqim representa as regras de negócio que guiam o comportamento da IA e das operações das empresas.

---

## 🧩 Agregados

### 📦 Catalog

Responsável por representar o que a empresa oferece.

#### Entidades:

- CatalogServiceItem
  - Representa serviços oferecidos
  - Ex: limpeza de pele, consulta, manutenção

- CatalogProduct
  - Representa produtos vendáveis
  - Ex: cosméticos, suplementos, etc

---

## 🔐 Regras do domínio

- Serviço e produto são conceitos distintos
- Não misturar regras entre eles
- Cada entidade deve possuir `TenantId`
- O domínio não depende de EF Core

---

## 🧱 Princípios

- Domínio é puro (sem dependência de infra)
- Regras de negócio ficam no domínio ou application
- Controllers nunca contêm regra

---

## 🧭 Evoluções previstas

- Categorias de serviços/produtos
- Precificação dinâmica
- Promoções
- Regras de disponibilidade
- Recomendação via IA

---

## ⚠️ Importante

- Não colocar lógica de banco aqui
- Não usar DbContext
- Não usar atributos de persistência (quando possível evitar)