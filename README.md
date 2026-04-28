# GameStore MVC — CP5 C# Development

Loja de games (e-commerce) desenvolvida com **ASP.NET Core 8 MVC**, MySQL e Bootstrap 5.

## Grupo

| Nome | RM |
|:--|:--:|
| Thiago Araujo Vieira | 553477 |
| Lucas Reis Diniz | 552838 |
| Diana Letícia | 553562 |
| João Viktor | 552613 |
| Victor Augusto | 553518 |
| Vitor de Moura | 553806 |

---

## Tecnologias

| Camada | Tecnologia |
|:--|:--|
| Framework | ASP.NET Core 8 MVC |
| Banco de dados | MySQL 8 via Pomelo EF Core |
| Front-end | Bootstrap 5 (responsivo) |
| Segurança | BCrypt.Net-Next + Cookie Authentication + Claims |
| ORM | Entity Framework Core 8 |

---

## Arquitetura

```
GameStoreMVC/
├── Controllers/
│   ├── HomeController.cs          # Página inicial e filtro por categoria
│   ├── AccountController.cs       # Login, cadastro e logout
│   └── GameController.cs          # CRUD de games (somente Admin)
├── Models/
│   ├── Game.cs
│   ├── Usuario.cs
│   └── ViewModels/
│       ├── LoginViewModel.cs
│       ├── RegisterViewModel.cs
│       └── GameViewModel.cs
├── Interfaces/
│   ├── IGameRepository.cs
│   └── IUsuarioRepository.cs
├── Repositories/
│   ├── GameRepository.cs
│   └── UsuarioRepository.cs
├── Data/
│   └── AppDbContext.cs
└── Views/
    ├── Home/Index.cshtml           # Hero + categorias + cards de games
    ├── Account/Login.cshtml
    ├── Account/CriarConta.cshtml
    ├── Game/Index.cshtml           # Painel admin
    ├── Game/Criar.cshtml
    ├── Game/Editar.cshtml
    └── Shared/_Layout.cshtml      # Navbar + Footer
```

---

## Funcionalidades

### Usuários
- Cadastro com nome, e-mail, senha e confirmação de senha
- Senhas armazenadas com hash **BCrypt**
- Login com autenticação via **Claims** (cookie)
- Logout seguro

### Loja (Home)
- Banner principal "Domine o Jogo"
- Filtro por categoria: **RPG · Ação · Corrida · Aventura**
- Seção **Em Destaque** com cards de games
- Exibição de preço, capa e botão "Comprar"
- Layout 100% responsivo (mobile-first)

### Admin — Gestão de Games
- Acesso restrito ao papel `Admin` via `[Authorize(Roles = "Admin")]`
- Cadastrar novo jogo (nome, descrição, preço, categoria, URL da capa, destaque)
- Editar jogo existente
- Excluir jogo com confirmação
- Botões Editar/Excluir visíveis na home quando logado como admin

---

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- MySQL 8+
- [EF Core CLI](https://learn.microsoft.com/ef/core/cli/dotnet) — `dotnet tool install -g dotnet-ef`

---

## Como rodar

### 1. Configurar o banco de dados

Edite `GameStoreMVC/appsettings.json` com suas credenciais MySQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=gamestore_db;User=root;Password=SUA_SENHA;"
}
```

### 2. Instalar dependências

```bash
cd GameStoreMVC
dotnet restore
```

### 3. Criar e aplicar migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Rodar o projeto

```bash
dotnet run
```

Acesse **http://localhost:5128**

---

## Usuário admin padrão

Criado automaticamente na primeira execução:

| Campo | Valor |
|:--|:--|
| E-mail | `admin@gamestore.com` |
| Senha | `Admin@123` |

---

## Segurança

- Senhas nunca armazenadas em texto plano — sempre via `BCrypt.HashPassword()`
- Autenticação por cookie com expiração de 8 horas
- Autorização por Claims: role `Admin` ou `User`
- `[ValidateAntiForgeryToken]` em todos os formulários POST
- Acesso ao CRUD de games bloqueado para usuários não-admin

---

## Commits semânticos

O projeto segue o padrão [Conventional Commits](https://www.conventionalcommits.org/) com mínimo de **10 commits**, um por camada:

| # | Mensagem |
|:--|:--|
| 1 | `feat: add ASP.NET Core MVC project setup with MySQL and BCrypt` |
| 2 | `feat: add Game and Usuario models with ViewModels and data annotations` |
| 3 | `feat: add repository interfaces for Game and Usuario` |
| 4 | `feat: add AppDbContext with Fluent API and MySQL configuration` |
| 5 | `feat: implement GameRepository and UsuarioRepository with EF Core async` |
| 6 | `feat: implement AccountController with BCrypt hashing and Claims authentication` |
| 7 | `feat: implement HomeController and GameController with admin CRUD` |
| 8 | `feat: add shared layout with Bootstrap 5 dark gaming theme and static assets` |
| 9 | `feat: add Login and CriarConta authentication views` |
| 10 | `feat: add home page and full game management views` |
