# 🛠️ InternalHelpDesk.API

API REST desenvolvida em **ASP.NET Core** para gerenciamento de chamados internos de TI com **fila de prioridade implementada com Heap**.

O sistema simula um Help Desk corporativo em que os chamados não são atendidos apenas por ordem de chegada. Chamados com maior impacto para a empresa, como indisponibilidade de servidor, rede ou sistemas internos, devem ter prioridade sobre solicitações individuais, como troca de mouse ou dúvidas pontuais.

---

## 📌 Cenário do projeto

| Item | Descrição |
|---|---|
| Time | Help Desk de TI Interno |
| Recurso principal | Chamado de TI |
| Endpoint base | `/api/Chamados/chamados-ti` |
| Estrutura de dados | Fila de prioridade com Heap |
| Banco de dados | SQL Server via Docker Compose |

O projeto atende à proposta do trabalho de **API com Filas de Prioridade e Heaps**, contemplando API REST, persistência em banco, Swagger/OpenAPI, exclusão lógica, regra explícita de prioridade, tratamento de empate e separação em camadas.

---

## 🚀 Tecnologias utilizadas

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Docker e Docker Compose
- Swagger / OpenAPI
- xUnit
- Moq
- FluentValidation
- Mapster

---

## 🧱 Estrutura da solução

```text
├── InternalHelpDesk.API
│   └── Controllers, Swagger e configurações da API.
│
├── InternalHelpDeskApi.Application
│   └── DTOs, validações, interfaces dos casos de usos e os casos de uso.
│
├── InternalHelpDeskApi.Domain
│   └── Entidades, enums, interfaces dos repositorios e regras de domínio.
│
├── InternalHelpDeskApi.Infrastructure
│   └── DbContext, migrations, repositórios e configurações do banco.
│
└── InternalHelpDeskApi.Tests
    └── Testes unitários da aplicação, prioridade e fila.
```

A solução segue uma organização inspirada em **Clean Architecture** e **DDD**:

- **API:** recebe requisições HTTP e retorna as respostas.
- **Application:** coordena os casos de uso.
- **Domain:** concentra entidades, enums e regra de prioridade.
- **Infrastructure:** implementa banco de dados, repositórios e migrations.
- **Tests:** valida regras de prioridade, fila, validações e controller.

---

## ⚖️ Regra de prioridade

A prioridade do chamado é calculada a partir dos pesos da **categoria** e da **prioridade** vinculadas ao chamado.

```csharp
pesoChamado = (pesoCategoria * 10) + pesoPrioridade;
```

Quanto maior o valor calculado, maior a prioridade do chamado na fila.

### Exemplo usando pesos

| Chamado | Peso da categoria | Peso da prioridade | Cálculo | Peso final | Ordem esperada |
|---|---:|---:|---:|---:|---|
| Servidor principal caiu | 3 | 10 | `(3 * 10) + 10` | 40 | 1º atendimento |
| Rede instável no setor | 2 | 5 | `(2 * 10) + 5` | 25 | 2º atendimento |
| Troca de mouse | 1 | 1 | `(1 * 10) + 1` | 11 | 3º atendimento |

### Tratamento de empate

Quando dois chamados possuem o mesmo peso final, o projeto utiliza a **data de criação** como critério de desempate.

No código atual, o chamado com a data de criação mais recente fica à frente no empate, conforme a implementação do `ChamadoPriorityComparer` e os testes unitários do projeto.

---

## 🌳 Uso de Heap na fila de prioridade

A fila de prioridade é implementada por meio de uma estrutura de **Heap**.

O Heap mantém o chamado mais prioritário no topo da estrutura, evitando percorrer toda a lista para descobrir qual chamado deve ser atendido primeiro.

| Operação | Função | Complexidade |
|---|---|---:|
| Consultar próximo chamado | Acessa o topo do Heap | `O(1)` |
| Inserir chamado | Insere e reorganiza a fila | `O(log n)` |

Uma fila FIFO comum não seria suficiente, pois atenderia apenas pela ordem de chegada. No Help Desk interno, um chamado crítico aberto depois de uma solicitação simples ainda precisa ser atendido primeiro.

---

## 🔗 Endpoints e exemplos de uso

URL base com Docker:

```text
http://localhost:8080
```

> Observação: endpoints `GET` e `DELETE` não recebem JSON no corpo da requisição. Os exemplos JSON abaixo são usados nos endpoints `POST` e `PUT`.

### Chamados de TI

| Método | Endpoint | Descrição |
|---|---|---|
| `POST` | `/api/Chamados/chamados-ti` | Cadastra um novo chamado. |
| `GET` | `/api/Chamados/chamados-ti/{id}` | Busca um chamado pelo ID. |
| `GET` | `/api/Chamados/chamados-ti?pageNumber=1&pageSize=10` | Lista chamados paginados. |
| `GET` | `/api/Chamados/chamados-ti/buscar?cpf=12345678900` | Busca chamados pelo CPF do solicitante. |
| `GET` | `/api/Chamados/chamados-ti/buscar?descricao=servidor` | Busca chamados pela descrição. |
| `GET` | `/api/Chamados/chamados-ti/proximo` | Consulta o chamado mais urgente. |
| `POST` | `/api/Chamados/chamados-ti/proximo/atender?id=1` | Distribui o próximo chamado para o atendente informado. |
| `GET` | `/api/Chamados/chamados-ti/ordenados` | Lista chamados ordenados por prioridade. |
| `PUT` | `/api/Chamados/chamados-ti/{id}` | Atualiza um chamado existente. |
| `DELETE` | `/api/Chamados/chamados-ti/{id}` | Realiza exclusão lógica do chamado. |

#### JSON para criar chamado

```json
{
  "titulo": "Servidor principal caiu",
  "descricao": "Problema crítico afetando toda a empresa.",
  "categoriaID": 1,
  "prioridadeId": 1,
  "status": 1,
  "solicitanteID": 1,
  "atendente": null
}
```

#### JSON para atualizar chamado

```json
{
  "titulo": "Servidor normalizado",
  "descricao": "Chamado atualizado após correção do problema.",
  "categoriaID": 1,
  "prioridadeId": 1,
  "status": 2,
  "solicitanteID": 1,
  "atendente": null
}
```

---

### Categorias

| Método | Endpoint | Descrição |
|---|---|---|
| `POST` | `/api/chamado-ti/categorias` | Cadastra uma categoria. |
| `GET` | `/api/chamado-ti/categorias/{id}` | Busca uma categoria pelo ID. |
| `GET` | `/api/chamado-ti/categorias?pageNumber=1&pageSize=10` | Lista categorias paginadas. |
| `PUT` | `/api/chamado-ti/categorias/{id}` | Atualiza uma categoria. |
| `DELETE` | `/api/chamado-ti/categorias/{id}` | Inativa uma categoria. |

#### JSON para criar categoria

```json
{
  "nome": "Suporte Técnico",
  "peso": 1
}
```

#### JSON para atualizar categoria

```json
{
  "id": 1,
  "nome": "Rede e Conectividade",
  "peso": 3
}
```

---

### Prioridades

| Método | Endpoint | Descrição |
|---|---|---|
| `POST` | `/api/chamado-ti/prioridades` | Cadastra uma prioridade. |
| `GET` | `/api/chamado-ti/prioridades/{id}` | Busca uma prioridade pelo ID. |
| `GET` | `/api/chamado-ti/prioridades?pageNumber=1&pageSize=10` | Lista prioridades paginadas. |
| `PUT` | `/api/chamado-ti/prioridades/{id}` | Atualiza uma prioridade. |
| `DELETE` | `/api/chamado-ti/prioridades/{id}` | Inativa uma prioridade. |

#### JSON para criar prioridade

```json
{
  "categoriaId": 1,
  "descricao": "Crítica",
  "peso": 5
}
```

#### JSON para atualizar prioridade

```json
{
  "id": 1,
  "categoriaId": 1,
  "descricao": "Muito Crítica",
  "peso": 10
}
```

---

### Atendentes

| Método | Endpoint | Descrição |
|---|---|---|
| `POST` | `/api/chamado-ti/Atendentes` | Cadastra um atendente. |
| `GET` | `/api/chamado-ti/Atendentes/{id}` | Busca um atendente pelo ID. |
| `GET` | `/api/chamado-ti/Atendentes?pageNumber=1&pageSize=10` | Lista atendentes paginados. |
| `PUT` | `/api/chamado-ti/Atendentes/{id}` | Atualiza um atendente. |
| `DELETE` | `/api/chamado-ti/Atendentes/{id}` | Inativa um atendente. |

#### JSON para criar atendente

```json
{
  "nome": "João Silva",
  "email": "joao.silva@empresa.com",
  "ativo": true
}
```

#### JSON para atualizar atendente

```json
{
  "id": 1,
  "nome": "João Pedro Silva",
  "email": "joao.pedro@empresa.com",
  "ativo": true
}
```

---

### Solicitantes

| Método | Endpoint | Descrição |
|---|---|---|
| `POST` | `/api/chamado-ti/solicitantes` | Cadastra um solicitante. |
| `GET` | `/api/chamado-ti/solicitantes/{id}` | Busca um solicitante pelo ID. |
| `GET` | `/api/chamado-ti/solicitantes?pageNumber=1&pageSize=10` | Lista solicitantes paginados. |
| `PUT` | `/api/chamado-ti/solicitantes/{id}` | Atualiza um solicitante. |
| `DELETE` | `/api/chamado-ti/solicitantes/{id}` | Inativa um solicitante. |

#### JSON para criar solicitante

```json
{
  "nome": "Carlos Alberto",
  "email": "carlos.alberto@empresa.com",
  "cpf": "12345678900",
  "ativo": true
}
```

#### JSON para atualizar solicitante

```json
{
  "id": 1,
  "nome": "Carlos Alberto Pereira",
  "email": "carlos.pereira@empresa.com",
  "cpf": "12345678900",
  "ativo": true
}
```

---

## 🧪 Ordem recomendada para testar

Para criar um chamado, é necessário possuir registros válidos de categoria, prioridade e solicitante.

Sugestão de teste no Swagger:

1. Criar uma categoria.
2. Criar uma prioridade vinculada à categoria.
3. Criar um solicitante.
4. Criar um atendente.
5. Criar um chamado de TI.
6. Consultar `/api/Chamados/chamados-ti/proximo`.
7. Distribuir o chamado com `/api/Chamados/chamados-ti/proximo/atender?id=1`.
8. Excluir logicamente um chamado com `DELETE`.

Os arquivos JSON de apoio estão em:

```text
InternalHelpDeskApi.Tests/TestsJson
├── Atendentes
├── Categorias
├── Prioridades
└── Solicitantes
```

---

## 🐳 Como executar com Docker

### 1. Clonar o repositório

```powershell
git clone https://github.com/i4nzz/InternalHelpDeskApi---Ricardo-Fulgencio.git
```

### 2. Acessar a pasta do projeto

```powershell
cd InternalHelpDeskApi---Ricardo-Fulgencio
```

### 3. Subir API e banco de dados

```powershell
docker compose up -d --build
```

### 4. Acessar o Swagger

```text
http://localhost:8080/swagger
```

---

## 🗄️ Banco de dados

O banco utilizado é o **SQL Server**, executado via Docker Compose.

Serviços criados:

```text
internalhelpdesk-api
internalhelpdesk-sqlserver
```

Portas utilizadas:

```text
API:        8080
SQL Server: 1433
```

Connection string no ambiente Docker:

```text
Server=sqlserver,1433;Database=FulgencioDB;User Id=sa;Password=Senha_Forte_12345;TrustServerCertificate=True;
```

As migrations são aplicadas automaticamente ao iniciar a aplicação.

---

## 💻 Como executar sem Docker

### 1. Restaurar pacotes

```powershell
dotnet restore .\InternalHelpDeskApi.sln
```

### 2. Compilar a solução

```powershell
dotnet build .\InternalHelpDeskApi.sln
```

### 3. Aplicar migrations

```powershell
dotnet ef database update --project .\InternalHelpDeskApi.Infrastructure\InternalHelpDeskApi.Infrastructure.csproj --startup-project .\InternalHelpDesk.API\InternalHelpDesk.API.csproj
```

### 4. Executar a API

```powershell
dotnet run --project .\InternalHelpDesk.API\InternalHelpDesk.API.csproj
```

Depois, acesse o Swagger pela URL exibida no terminal.

---

## 🧪 Testes unitários

O projeto possui testes unitários com **xUnit** e **Moq**, cobrindo:

- regra de prioridade;
- funcionamento da fila com Heap;
- tratamento de empate;
- validações de DTO;
- criação, atualização e exclusão lógica de chamados;
- retornos principais do controller.

Para executar:

```powershell
dotnet test .\InternalHelpDeskApi.sln
```

Resultado esperado:

```text
Total: 26 | Falhas: 0 | Sucesso: 26
```

---

## ✅ Observações finais

- O Swagger está disponível em `/swagger` quando a aplicação roda em ambiente de desenvolvimento.
- A exclusão de chamados é lógica: o chamado tem o status alterado para `Cancelado`.
- A fila de prioridade considera apenas chamados com status `Aberto`.
- Categorias, prioridades, atendentes e solicitantes também possuem inativação lógica pelo campo `ativo`.
- O endpoint `/api/Chamados/chamados-ti/ordenados` demonstra a aplicação prática da fila de prioridade com Heap.
