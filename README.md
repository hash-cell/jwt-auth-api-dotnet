# 🔐 LoginJWT API (.NET)

API de autenticação desenvolvida com **C# e ASP.NET Core**, utilizando **JWT (JSON Web Token)** e **Refresh Token**, seguindo boas práticas de arquitetura como **Service Layer, Interfaces e Middleware Global**.

---

## 🚀 Tecnologias utilizadas

* C#
* ASP.NET Core
* Entity Framework Core
* JWT (JSON Web Token)
* PostgreSQL / SQL Server (adaptável)
* BCrypt (hash de senha)

---

## 🧠 Arquitetura

O projeto foi estruturado seguindo boas práticas de separação de responsabilidades:

```
Controllers → Services → TokenService → Database
```

### 📦 Camadas:

* **Controllers**

  * Responsáveis por receber as requisições HTTP

* **Services**

  * Contêm a lógica de negócio (AuthService)

* **TokenService**

  * Responsável exclusivamente pela geração de tokens

* **Interfaces**

  * Contratos que garantem desacoplamento e escalabilidade

* **Middleware**

  * Tratamento global de erros

---

## 🔐 Funcionalidades

* ✅ Registro de usuário
* ✅ Login com JWT
* ✅ Geração de Refresh Token
* ✅ Renovação de Token (Refresh)
* ✅ Senhas criptografadas com BCrypt
* ✅ Middleware global para tratamento de erros
* ✅ Estrutura pronta para roles/permissions

---

## 🔄 Fluxo de autenticação

1. Usuário faz login
2. API valida credenciais
3. Gera:

   * Access Token (curta duração)
   * Refresh Token (longa duração)
4. Cliente usa o Access Token nas requisições
5. Quando expira, usa o Refresh Token para gerar um novo

---

## ⚙️ Como rodar o projeto

```bash
# Restaurar dependências
dotnet restore

# Rodar aplicação
dotnet run
```

---

## 🔑 Configuração (appsettings.json)

```json
"Jwt": {
  "Key": "SUA_CHAVE_SECRETA",
  "Issuer": "seu-sistema",
  "Audience": "seu-sistema"
},
"Pepper": "seu-pepper-secreto"
```

---

## 🧪 Testes

Você pode testar os endpoints usando:

* Swagger (já integrado)
* Postman
* Insomnia

---

## 📌 Melhorias futuras

* 🔐 Implementação de Roles e Permissions
* 📊 Logs estruturados
* 🧪 Testes automatizados
* 🔒 Validação avançada de tokens
* 📦 Dockerização

---

## 👨‍💻 Autor

Desenvolvido por Victor Prado 🚀
Focado em evolução constante como desenvolvedor backend.

---
