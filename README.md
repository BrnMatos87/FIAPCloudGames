<<<<<<< HEAD
# FIAPCloudGames
=======
# 🎮 FIAPCloudGames API

API REST desenvolvida em **.NET 8** para estruturar a FIAP Cloud Gmes (FGC). Contanto com o cadastro de usuários e biblioteca de jogos 
adquiridos. Aplicando boas práticas de arquitetura, segurança e observabilidade.
 
---

# 📌 Objetivo do Projeto

Este projeto tem como objetivo desenvolver uma API que permita:

* Gerenciar usuários do sistema
* Gerenciar jogos disponíveis
* Controlar a biblioteca de jogos de cada usuário
* Implementar autenticação e autorização com diferentes níveis de acesso

Além disso, o projeto busca aplicar conceitos importantes como:

* Arquitetura em camadas
* Domain-Driven Design (DDD)
* Segurança com JWT
* Validação de dados
* Logging e rastreabilidade (CorrelationId)

---

# 🧠 Arquitetura

A aplicação foi estruturada com separação de responsabilidades:

```id="arch1"
Core
 ├── Entity
 ├── Enum
 ├── Repository (Interfaces)
 ├── Service

Infrastructure
 ├── Correlation
 ├── Logging
 ├── Middleware
 ├── Repository (EF Core)

API
 ├── Controllers
 ├── Configurations

```

---

# 🚀 Tecnologias Utilizadas

* .NET 8 (ASP.NET Core)
* Entity Framework Core
* SQL Server
* JWT (Json Web Token)
* Swagger (OpenAPI)
* BCrypt (hash de senha)

---

# 🔐 Autenticação e Autorização

A API utiliza **JWT (Bearer Token)** para autenticação.

## Perfis de acesso:

### 👑 Administrador

* Gerenciar usuários
* Gerenciar jogos

### 👤 Usuário

* Gerenciar sua própria biblioteca de jogos

---

# 📌 Funcionalidades

## 👤 Usuários (Administrador)

* Criar usuário
* Atualizar dados
* Alterar senha
* Alterar perfil
* Inativar usuário
* Ativar usuário
* Listar usuários (resumo e completo)
* Obter por Id (resumo e completo)

---

## 🎮 Jogos (Administrador)

* Criar jogo
* Atualizar jogo
* Aplicar promoção
* Remover promoção
* Inativar jogo
* Ativar jogo
* Listar jogos (resumo e completo)
* Obter por Id (resumo e completo)
---

## 📚 Biblioteca de Jogos (Usuário)

* Criar biblioteca
* Inativar biblioteca
* Ativar biblioteca
* Listar bibliotecas
* Obter biblioteca
---

# 🗄️ Banco de Dados

A aplicação utiliza **SQL Server**, **Entity Framework Core** e migrations.

## Entidades principais:

* Usuario
* Jogo
* BibliotecaJogo

---
# 🧪 Testes

O projeto possui testes unitários para validar as principais regras de negócio.

## 🧪 Tecnologias utilizadas nos testes

- **xUnit** – Framework de testes unitários
- **Moq** – Biblioteca para criação de mocks
---

# ⚙️ Configuração

No arquivo `appsettings.json`, configure:

```json id="config1"
{
  "ConnectionStrings": {
    "ConnectionString": "CONNECTION_STRING"
  },
  "Jwt": {
    "Key": "CHAVE_SECRETA",
    "Issuer": "FIAPCloudGames",
    "Audience": "FIAPCloudGamesClient"
  }
}
```

---

# ▶️ Como Executar o Projeto

## 1. Clonar o repositório

```id="run1"
https://github.com/BrnMatos87/FIAPCloudGames.git
```

## 2. No SQL Server 

```id="run2"
Criar banco de dados e usuário de aplicação
```

## 3. Configurar a ConnetionString

```id="run3"
No appsettings.json
Configurar: 
  "ConnectionStrings": {
    "ConnectionString": "SUA_CONNECTIONSTRING"
  }
```

## 4. Configurar o Token JWT

```id="run4"
No appsettings.json
Configurar: 
 "Jwt": {
  "Key": "SUACHAVE",
  "Issuer": "FIAPCloudGames",
  "Audience": "FIAPCloudGamesClient"
},
```

## 5. Aplicar migrations

```id="run5"
Update-database -StartupProject Infrastructure -connection "SUA_CONNECTIONSTRING"
```
---

# 📖 Documentação da API (Swagger)

Após executar o projeto, acesse:

```id="swagger1"
https://localhost:{porta}/swagger
```

---

# 🔑 Como Utilizar a API

## 1. Realizar login

```http id="api1"
POST /api/v1/auth/login
```

### Body:

```json id="api2"
{
  "email": "admin@email.com",
  "senha": "123456"
}
```

---

## 2. Copiar o token retornado

```json id="api3"
{
  "token": "TOKEN_AQUI"
}
```

---

## 3. Autenticar no Swagger

Clique em **Authorize** e informe:

```id="api4"
TOKEN_AQUI
```

---

## 4. Consumir os endpoints protegidos

Agora você poderá acessar:

* Usuários (Admin)
* Jogos (Admin)
* Biblioteca (Usuário)

---

# 📊 Padrões REST utilizados

* `GET` → 200 OK
* `POST` → 201 Created
* `PUT/PATCH` → 204 NoContent
* `DELETE` → 204 NoContent

---

# 🔍 Observabilidade

A aplicação possui:

* Logging estruturado
* CorrelationId por requisição
* Middleware global de exceções

---

# 🛡️ Segurança

* Senhas protegidas com **BCrypt**
* Autenticação via JWT
* Controle de acesso por perfil
* Validação de dados com DataAnnotations

---

# 📌 Considerações Finais

O projeto foi desenvolvido com foco em:

* Organização e boas práticas
* Segurança da aplicação
* Facilidade de manutenção
* Clareza na separação de responsabilidades

---

# 👨‍💻 Autor

Projeto desenvolvido para fins acadêmicos por Bruno Campos.
>>>>>>> fase1
