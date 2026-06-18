# InternalHelpDesk.API

API desenvolvida em ASP.NET Core para gerenciamento de chamados internos de TI.

O sistema permite cadastrar, consultar, atualizar, excluir logicamente e priorizar chamados com base em critérios como categoria, prioridade e data de criação.

## Tecnologias utilizadas

* C#
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Docker
* Docker Compose
* Swagger / OpenAPI
* xUnit
* Moq
* FluentValidation
* Mapster

## Estrutura do projeto

```text
InternalHelpDeskApi
├── InternalHelpDesk.API
│   └── Camada de apresentação da API, controllers, Swagger e configurações.
│
├── InternalHelpDeskApi.Application
│   └── DTOs, validações e casos de uso da aplicação.
│
├── InternalHelpDeskApi.Domain
│   └── Entidades, enums, interfaces e regras de domínio.
│
├── InternalHelpDeskApi.Infrastructure
│   └── Banco de dados, DbContext, migrations, repositórios e injeção de dependência.
│
└── InternalHelpDeskApi.Tests
    └── Testes unitários da aplicação.
```

## Funcionalidades principais

* Cadastro de chamados de TI.
* Consulta de chamados por ID.
* Listagem paginada de chamados.
* Busca por CPF do solicitante ou descrição.
* Atualização de chamados.
* Exclusão lógica de chamados.
* Consulta do chamado mais urgente.
* Distribuição do próximo chamado prioritário.
* Listagem de chamados ordenados por prioridade.
* Documentação dos endpoints via Swagger.

## Regra de prioridade

A prioridade do chamado considera o peso da categoria e o peso da prioridade.

A regra aplicada no comparador calcula o peso lógico do chamado e define qual item deve ser atendido primeiro.

Exemplo de cenário:

* Um problema que afeta toda a empresa, como queda do servidor principal, deve ter prioridade maior.
* Um problema individual, como troca de mouse, deve ter prioridade menor.

Quando dois chamados possuem o mesmo peso, é aplicado um critério de desempate pela data de criação.

## Pré-requisitos

Para rodar o projeto localmente, é necessário ter instalado:

* .NET 8 SDK
* Docker Desktop
* Git

Para rodar sem Docker, também é necessário ter SQL Server ou SQL Server LocalDB instalado.

## Como rodar com Docker

Clone o repositório:

```powershell
git clone URL_DO_REPOSITORIO
```

Acesse a pasta do projeto:

```powershell
cd InternalHelpDeskApi---Ricardo-Fulgencio
```

Suba a API e o banco SQL Server com Docker Compose:

```powershell
docker compose up -d --build
```

Aguarde alguns segundos até os containers iniciarem.

Acesse o Swagger:

```text
http://localhost:8080/swagger
```

## Containers criados

O `docker-compose.yml` sobe dois serviços principais:

```text
internalhelpdesk-api
internalhelpdesk-sqlserver
```

A API roda na porta:

```text
8080
```

O SQL Server roda na porta:

```text
1433
```

## Connection string no Docker

Dentro do Docker, a API se conecta ao SQL Server usando o nome do serviço definido no `docker-compose.yml`.

```text
Server=sqlserver,1433;Database=FulgencioDB;User Id=sa;Password=Senha_Forte_12345;TrustServerCertificate=True;
```

## Rodar migrations

O projeto está configurado para aplicar migrations automaticamente ao iniciar a API.

Caso seja necessário aplicar manualmente, execute:

```powershell
dotnet ef database update --project .\InternalHelpDeskApi.Infrastructure\InternalHelpDeskApi.Infrastructure.csproj --startup-project .\InternalHelpDesk.API\InternalHelpDesk.API.csproj
```

## Como rodar localmente sem Docker

Acesse a pasta do projeto:

```powershell
cd InternalHelpDeskApi---Ricardo-Fulgencio
```

Restaure os pacotes:

```powershell
dotnet restore .\InternalHelpDeskApi.sln
```

Compile a solução:

```powershell
dotnet build .\InternalHelpDeskApi.sln
```

Aplique as migrations:

```powershell
dotnet ef database update --project .\InternalHelpDeskApi.Infrastructure\InternalHelpDeskApi.Infrastructure.csproj --startup-project .\InternalHelpDesk.API\InternalHelpDesk.API.csproj
```

Execute a API:

```powershell
dotnet run --project .\InternalHelpDesk.API\InternalHelpDesk.API.csproj
```

Acesse o Swagger na URL exibida no terminal.

## Como rodar os testes

Execute:

```powershell
dotnet test .\InternalHelpDeskApi.sln
```

Resultado esperado:

```text
Resumo do teste: total: 26; falhou: 0; bem-sucedido: 26; ignorado: 0
```

## Endpoints principais

### Listar chamados paginados

```http
GET /api/Chamados/chamados-ti?pageNumber=1&pageSize=10
```

Lista os chamados ativos de forma paginada.

### Consultar chamado por ID

```http
GET /api/Chamados/chamados-ti/{id}
```

Consulta um chamado específico.

### Criar chamado

```http
POST /api/Chamados/chamados-ti
```

Exemplo de corpo:

```json
{
  "titulo": "Servidor principal caiu",
  "descricao": "Problema crítico afetando toda a empresa.",
  "categoriaID": 1,
  "prioridadeId": 1,
  "status": 0,
  "solicitanteID": 1,
  "atendente": null
}
```

### Atualizar chamado

```http
PUT /api/Chamados/chamados-ti/{id}
```

Atualiza os dados de um chamado existente.

### Excluir chamado logicamente

```http
DELETE /api/Chamados/chamados-ti/{id}
```

Realiza exclusão lógica do chamado.

### Buscar por CPF ou descrição

```http
GET /api/Chamados/chamados-ti/buscar?cpf=12345678900
```

ou:

```http
GET /api/Chamados/chamados-ti/buscar?descricao=servidor
```

### Consultar próximo chamado urgente

```http
GET /api/Chamados/chamados-ti/proximo
```

Retorna o chamado mais urgente conforme a regra de prioridade.

### Distribuir próximo chamado para atendimento

```http
POST /api/Chamados/chamados-ti/proximo/atender?id=1
```

Distribui o próximo chamado prioritário para o atendente informado.

### Listar chamados ordenados por prioridade

```http
GET /api/Chamados/chamados-ti/ordenados
```

Retorna os chamados ordenados pela regra de prioridade.

## Comandos úteis do Docker

Subir containers:

```powershell
docker compose up -d --build
```

Ver containers em execução:

```powershell
docker ps
```

Ver logs da API:

```powershell
docker compose logs api
```

Ver logs do SQL Server:

```powershell
docker compose logs sqlserver
```

Parar containers:

```powershell
docker compose down
```

Parar containers e apagar volume do banco:

```powershell
docker compose down -v
```

## Observações

* O Swagger está disponível em `/swagger`.
* O banco utilizado no Docker é SQL Server.
* As migrations são aplicadas automaticamente na inicialização da API.
* O projeto possui testes unitários cobrindo regras de prioridade, fila, validações, casos de uso e controller.
* A exclusão de chamados é lógica, ou seja, o registro não é necessariamente removido fisicamente do banco.

## Autor

Projeto acadêmico desenvolvido para simular um sistema interno de Help Desk de TI.
