# Order Management API

Uma **API REST em .NET** para gerenciamento simples de pedidos, desenvolvida com foco em boas práticas de engenharia de software.

Este projeto utiliza **Clean Architecture** e padrões comuns em sistemas corporativos, servindo como um projeto de portfólio para demonstrar organização de código, escalabilidade e desenvolvimento de APIs profissionais.

---

## 📖 Descrição

A API permite gerenciar o ciclo de vida de pedidos e seus itens, incluindo criação, consulta detalhada e atualização de status.

### 🏗️ Arquitetura e Fluxo
O sistema segue os princípios da Clean Architecture para garantir o desacoplamento:

`Client` → `API Controllers` → `Application Services` → `Domain` → `Infrastructure` → `Database`

### 🛠️ Principais Operações
* **Pedidos:** Criar, consultar, atualizar status e cancelar.
* **Itens:** Adicionar itens a pedidos existentes.
* **Listagem:** Listagem de pedidos com suporte a paginação e filtros.

---

## 📦 Pré-requisitos

Você pode rodar o projeto de duas formas:

1.  **Docker (Recomendado):** Requer [Docker](https://www.docker.com/) e [Docker Compose](https://docs.docker.com/compose/).
2.  **Localmente:** Requer [.NET SDK](https://dotnet.microsoft.com/), Git e um banco de dados compatível (SQL Server, PostgreSQL ou SQLite).

---