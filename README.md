# OpenBankingAPI

## Introdução

Exemplo de estrutura simples de implementação de uma Open Banking API feita com ASP.NET Core e EF Core

## Arquitetura

- SOLID
- OpenID Connect e OAuth 2.0
- Mediator
- CQRS
- Unit of Work
- Repository and Generic Repository

## Tecnologias

- ASP.NET Core 3
- ASP.NET MVC Core 
- ASP.NET WebApi Core
- Entity Framework Core
- IdentityServer4
- .NET Core Native DI
- AutoMapper
- MediatR
- Swagger UI

## Como usar

- Clonar o repositório
- Abrir solução no Visual Studio 2017 ou mais atual
- Definir solução para [mutiplos startups](https://docs.microsoft.com/pt-br/visualstudio/ide/how-to-set-multiple-startup-projects?view=vs-2019)
- Rodar o projeto OBAPI.IdentityServer
    - Implementação OpenID Connect e OAuth 2.0
- Rodar o projeto OBAPI.Web
    - Web API provedora dos dados protegidos
- Rodar o projeto MvcClient
    - Representação do sistema registrado como client para chamar WebApi mediante concentimento do usuário através da implementação do OAuth 2.0
    - Usuário registrado para teste
        - user: alice
        - password: alice



