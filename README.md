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
  "CategoriaId": 1,
  "prioridadeId": 1,
  "status": 0,
  "SolicitanteId": 1,
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

## Testes unitários

O projeto possui testes unitários implementados com **xUnit** e **Moq**, cobrindo as principais regras de domínio, validações, casos de uso e controller da API.

Para executar todos os testes, utilize:

```powershell
dotnet test .\InternalHelpDeskApi.sln
```

Resultado esperado:

```text
Resumo do teste: total: 26; falhou: 0; bem-sucedido: 26; ignorado: 0
```

Os testes estão organizados no projeto:

```text
InternalHelpDeskApi.Tests
├── API
│   └── Controllers
│       └── ChamadosControllerTests.cs
│
├── Application
│   ├── UseCases
│   │   ├── CriarChamadoUseCaseTests.cs
│   │   ├── SoftDeleteChamadoUseCaseTests.cs
│   │   └── UpdateChamadoUseCaseTests.cs
│   │
│   └── Validators
│       └── ChamadosDTOValidatorTests.cs
│
├── Domain
│   └── Services
│       ├── ChamadoPriorityComparerTests.cs
│       └── FilaPrioridadeHeapTests.cs
│
└── Helpers
    └── ChamadoFactory.cs
```

## Como funcionam os testes unitários

Testes unitários são testes pequenos e isolados, criados para validar uma parte específica do sistema.

No projeto, os testes verificam se cada classe se comporta corretamente sem depender diretamente de banco de dados real, API rodando ou interface externa.

Exemplo:

* O teste do comparador verifica se a regra de prioridade está funcionando.
* O teste da fila verifica se o chamado mais importante sai primeiro.
* O teste do validator verifica se dados inválidos são bloqueados.
* Os testes dos use cases verificam se os métodos chamam corretamente o repositório.
* Os testes do controller verificam se os endpoints retornam os status HTTP esperados.

Para isolar dependências, foi utilizado o **Moq**. Ele permite criar objetos falsos, chamados mocks, para simular comportamentos de interfaces como `IChamadoRepository` e os use cases utilizados pelo controller.

Assim, o teste valida somente a lógica da classe testada, sem depender de banco ou serviços externos.

## Explicação dos testes unitários implementados

### 1. ChamadoPriorityComparerTests

Arquivo:

```text
InternalHelpDeskApi.Tests/Domain/Services/ChamadoPriorityComparerTests.cs
```

Esses testes validam a regra de comparação de prioridade entre chamados.

A classe testada é:

```text
InternalHelpDeskApi.Domain/Services/ChamadoPriorityComparer.cs
```

Ela calcula a prioridade lógica de um chamado usando o peso da categoria e o peso da prioridade.

A regra usada é:

```csharp
int pesoX = (x.Categoria?.Peso ?? 0) * 10 + (x.Prioridade?.Peso ?? 0);
int pesoY = (y.Categoria?.Peso ?? 0) * 10 + (y.Prioridade?.Peso ?? 0);
```

Ou seja, chamados com categoria e prioridade de maior peso devem ter preferência na fila.

#### Teste: Compare_DeveSelecionarPrimeiroChamadoComMaiorPrioridade

Verifica se um chamado com maior peso de prioridade é considerado mais importante do que um chamado simples.

Exemplo de cenário:

* Chamado 1: servidor principal caiu.
* Chamado 2: troca de mouse.

Resultado esperado:

```text
O chamado do servidor deve ter prioridade maior.
```

#### Teste: Compare_DeveAplicarDesempatePorData_QuandoChamadosTiveremMesmaPrioridade

Verifica o critério de desempate quando dois chamados possuem o mesmo peso de prioridade.

Resultado esperado:

```text
Quando os pesos são iguais, a data de criação é usada como critério de desempate.
```

#### Teste: Compare_DeveRetornarZero_QuandoChamadosTiveremMesmoPesoEMesmaData

Verifica se dois chamados com mesmo peso e mesma data são considerados equivalentes na comparação.

Resultado esperado:

```text
O comparador deve retornar 0.
```

## 2. FilaPrioridadeHeapTests

Arquivo:

```text
InternalHelpDeskApi.Tests/Domain/Services/FilaPrioridadeHeapTests.cs
```

Esses testes validam o funcionamento da fila de prioridade implementada com heap.

A classe testada é:

```text
InternalHelpDeskApi.Domain/Services/FilaPrioridadeHeap.cs
```

A fila de prioridade é responsável por garantir que o chamado mais importante seja atendido primeiro.

### Como funciona o heap neste projeto

O heap é uma estrutura de dados usada para manter sempre o item mais prioritário no topo da fila.

No projeto, a fila usa uma lista interna:

```csharp
private readonly List<T> _heap = new();
```

Essa lista representa uma árvore binária de prioridade.

Cada item inserido na fila é comparado usando:

```csharp
private readonly IComparer<T> _comparer;
```

No caso dos chamados, o comparador utilizado é o `ChamadoPriorityComparer`.

### Enfileirar

Quando um item é inserido, ele é colocado no final da lista:

```csharp
_heap.Add(item);
```

Depois, o método `Subir` é chamado:

```csharp
Subir(_heap.Count - 1);
```

O método `Subir` compara o item inserido com seu pai. Se ele tiver prioridade maior, os dois trocam de posição.

Isso se repete até que o item fique na posição correta.

Em outras palavras:

```text
Quando um chamado urgente entra na fila, ele sobe até ficar acima dos chamados menos importantes.
```

### Desenfileirar

Quando um item é removido, a fila remove o item do topo:

```csharp
T topo = _heap[0];
```

Esse topo representa o chamado de maior prioridade.

Depois, o último item da lista é colocado no topo e o método `Descer` reorganiza a fila.

O método `Descer` compara o item com seus filhos e troca com o filho de maior prioridade até que a estrutura fique correta novamente.

Em outras palavras:

```text
Quando o chamado mais urgente sai da fila, a fila se reorganiza para colocar o próximo mais urgente no topo.
```

### Fila vazia

Se o método `Desenfileirar` for chamado quando a fila estiver vazia, o sistema lança uma exceção:

```csharp
throw new InvalidOperationException("Fila vazia.");
```

Isso evita que o sistema tente remover um item inexistente.

#### Teste: Enfileirar_DeveAdicionarChamadoNaFila

Verifica se, ao enfileirar um chamado, a fila deixa de estar vazia e a contagem passa a ser 1.

Resultado esperado:

```text
A fila deve conter um item.
```

#### Teste: Desenfileirar_DeveRetornarChamadoComMaiorPrioridadePrimeiro

Verifica se, ao inserir chamados com prioridades diferentes, o chamado de maior prioridade é retornado primeiro.

Resultado esperado:

```text
O chamado mais importante deve sair antes dos chamados menos importantes.
```

#### Teste: Desenfileirar_DeveAplicarDesempatePorData_QuandoChamadosTiveremMesmaPrioridade

Verifica se a fila respeita o critério de desempate quando dois chamados têm a mesma prioridade.

Resultado esperado:

```text
A fila deve usar a data de criação como critério de desempate.
```

#### Teste: Desenfileirar_DeveLancarExcecao_QuandoFilaEstiverVazia

Verifica se a fila lança uma exceção ao tentar remover um chamado quando não há itens.

Resultado esperado:

```text
A exceção "Fila vazia." deve ser lançada.
```

#### Teste: EstaVazia_DeveRetornarTrue_QuandoNaoExistiremChamados

Verifica se uma fila recém-criada está vazia.

Resultado esperado:

```text
EstaVazia deve retornar true e Contagem deve ser 0.
```

## 3. ChamadosDTOValidatorTests

Arquivo:

```text
InternalHelpDeskApi.Tests/Application/Validators/ChamadosDTOValidatorTests.cs
```

Esses testes validam as regras de entrada do DTO de chamados.

A classe testada é:

```text
InternalHelpDeskApi.Application/Validators/Chamados/ChamadosDTOValidator.cs
```

O validator usa FluentValidation para impedir que dados inválidos sejam enviados para a aplicação.

### Regras validadas

O DTO de chamado precisa respeitar as seguintes regras:

* O título é obrigatório.
* O título deve ter no máximo 100 caracteres.
* A descrição é obrigatória.
* A descrição deve ter pelo menos 10 caracteres.
* A categoria deve ser válida.
* A prioridade deve ser válida.
* O solicitante deve ser válido.
* O status deve ser um valor existente no enum.

#### Teste: Validate_DevePassar_QuandoDtoForValido

Verifica se um DTO preenchido corretamente passa na validação.

Resultado esperado:

```text
Nenhum erro de validação deve ocorrer.
```

#### Teste: Validate_DeveRetornarErro_QuandoTituloEstiverVazio

Verifica se o sistema bloqueia chamados sem título.

Resultado esperado:

```text
A mensagem "O título do chamado é obrigatório." deve ser retornada.
```

#### Teste: Validate_DeveRetornarErro_QuandoTituloTiverMaisDe100Caracteres

Verifica se o sistema bloqueia títulos muito longos.

Resultado esperado:

```text
A mensagem "O título deve ter no máximo 100 caracteres." deve ser retornada.
```

#### Teste: Validate_DeveRetornarErro_QuandoDescricaoEstiverVazia

Verifica se o sistema bloqueia chamados sem descrição.

Resultado esperado:

```text
A mensagem "A descrição do chamado é obrigatória." deve ser retornada.
```

#### Teste: Validate_DeveRetornarErro_QuandoDescricaoTiverMenosDe10Caracteres

Verifica se o sistema bloqueia descrições muito curtas.

Resultado esperado:

```text
A mensagem "A descrição deve ter pelo menos 10 caracteres." deve ser retornada.
```

#### Teste: Validate_DeveRetornarErro_QuandoCategoriaForInvalida

Verifica se o sistema bloqueia categoria com valor menor ou igual a zero.

Resultado esperado:

```text
A mensagem "Informe uma categoria válida." deve ser retornada.
```

#### Teste: Validate_DeveRetornarErro_QuandoPrioridadeForInvalida

Verifica se o sistema bloqueia prioridade com valor menor ou igual a zero.

Resultado esperado:

```text
A mensagem "Informe uma prioridade válida." deve ser retornada.
```

#### Teste: Validate_DeveRetornarErro_QuandoSolicitanteForInvalido

Verifica se o sistema bloqueia solicitante com valor menor ou igual a zero.

Resultado esperado:

```text
A mensagem "Informe um solicitante válido." deve ser retornada.
```

#### Teste: Validate_DeveRetornarErro_QuandoStatusForInvalido

Verifica se o sistema bloqueia status inexistente no enum.

Resultado esperado:

```text
A mensagem "O status fornecido é inválido." deve ser retornada.
```

## 4. CriarChamadoUseCaseTests

Arquivo:

```text
InternalHelpDeskApi.Tests/Application/UseCases/CriarChamadoUseCaseTests.cs
```

Esse teste valida o caso de uso responsável por criar chamados.

A classe testada é:

```text
InternalHelpDeskApi.Application/CriarChamadoUseCase.cs
```

O use case recebe um `ChamadosDto`, converte para a entidade `Chamados` usando Mapster e chama o repositório para salvar.

#### Teste: CriarChamado_DeveAdicionarChamadoERetornarChamadoCriado

Verifica se o use case chama o método `AddAsync` do repositório e retorna o chamado criado.

O repositório é simulado usando Moq, evitando a necessidade de acessar o banco de dados real.

Resultado esperado:

```text
O chamado deve ser criado e o método AddAsync deve ser chamado uma única vez.
```

## 5. SoftDeleteChamadoUseCaseTests

Arquivo:

```text
InternalHelpDeskApi.Tests/Application/UseCases/SoftDeleteChamadoUseCaseTests.cs
```

Esses testes validam o caso de uso responsável pela exclusão lógica de chamados.

A classe testada é:

```text
InternalHelpDeskApi.Application/SoftDeleteChamadoUseCases.cs
```

A exclusão lógica significa que o chamado não precisa ser removido fisicamente do banco. Em vez disso, ele pode ser marcado como excluído ou inativo conforme a regra do repositório.

#### Teste: SoftDeleteChamado_DeveExcluirLogicamente_QuandoChamadoExistir

Verifica se o chamado é buscado pelo ID e, caso exista, o método `SoftDeleteAsync` é chamado.

Resultado esperado:

```text
O chamado deve ser encontrado e excluído logicamente.
```

#### Teste: SoftDeleteChamado_DeveLancarExcecao_QuandoChamadoNaoExistir

Verifica se o sistema lança exceção ao tentar excluir um chamado inexistente.

Resultado esperado:

```text
A exceção "Chamado com ID {id} não encontrado." deve ser lançada.
```

Além disso, o teste garante que o método `SoftDeleteAsync` não seja chamado quando o chamado não existe.

## 6. UpdateChamadoUseCaseTests

Arquivo:

```text
InternalHelpDeskApi.Tests/Application/UseCases/UpdateChamadoUseCaseTests.cs
```

Esse teste valida o caso de uso responsável por atualizar chamados.

A classe testada é:

```text
InternalHelpDeskApi.Application/UpdateChamadoUseCase.cs
```

O use case recebe o ID do chamado e um DTO com os novos dados. Em seguida, converte o DTO para a entidade `Chamados`, define o ID informado e chama o repositório.

#### Teste: UpdateChamado_DeveAtualizarChamadoComIdInformado

Verifica se o use case chama o método `UpdateAsync` com o ID correto e com os dados atualizados.

Resultado esperado:

```text
O chamado deve ser enviado ao repositório com o ID informado e os dados atualizados.
```

## 7. ChamadosControllerTests

Arquivo:

```text
InternalHelpDeskApi.Tests/API/Controllers/ChamadosControllerTests.cs
```

Esses testes validam os retornos principais do controller de chamados.

A classe testada é:

```text
InternalHelpDesk.API/Controllers/ChamadosController.cs
```

O controller depende de vários use cases. Nos testes, esses use cases são simulados com Moq.

Assim, os testes verificam apenas se o controller retorna o status HTTP correto e chama o use case esperado.

#### Teste: CriarChamado_DeveRetornarCreatedAtAction_QuandoChamadoForCriado

Verifica se o endpoint de criação retorna `201 Created`.

Resultado esperado:

```text
O controller deve retornar CreatedAtAction com o chamado criado.
```

#### Teste: Update_DeveRetornarNoContent_QuandoChamadoForAtualizado

Verifica se o endpoint de atualização retorna `204 No Content`.

Resultado esperado:

```text
O controller deve chamar o use case de atualização e retornar NoContent.
```

#### Teste: Delete_DeveRetornarNoContent_QuandoChamadoForExcluidoLogicamente

Verifica se o endpoint de exclusão lógica retorna `204 No Content`.

Resultado esperado:

```text
O controller deve chamar o use case de exclusão lógica e retornar NoContent.
```

#### Teste: Buscar_DeveRetornarBadRequest_QuandoCpfEDescricaoNaoForemInformados

Verifica se o endpoint de busca retorna erro quando nenhum filtro é informado.

Resultado esperado:

```text
O controller deve retornar BadRequest com a mensagem solicitando CPF ou descrição.
```

#### Teste: DistribuirChamado_DeveRetornarOk_QuandoChamadoForDistribuido

Verifica se o endpoint responsável por distribuir o próximo chamado retorna `200 OK`.

Resultado esperado:

```text
O controller deve retornar Ok com o chamado distribuído.
```

## Helper dos testes

### ChamadoFactory

Arquivo:

```text
InternalHelpDeskApi.Tests/Helpers/ChamadoFactory.cs
```

O `ChamadoFactory` é uma classe auxiliar usada apenas nos testes.

Ela centraliza a criação de objetos `Chamados` e `ChamadosDto`, evitando repetição de código.

Sem o factory, cada teste precisaria criar manualmente um objeto completo:

```csharp
var chamado = new Chamados
{
    Id = 1,
    Titulo = "Servidor principal caiu",
    Descricao = "Problema crítico afetando toda a empresa.",
    CategoriaId = 1,
    PrioridadeId = 1,
    SolicitanteId = 1
};
```

Com o factory, o teste pode simplesmente usar:

```csharp
var chamado = ChamadoFactory.CriarChamado();
```

Ou, quando quiser alterar algum dado específico:

```csharp
var chamado = ChamadoFactory.CriarChamado(
    id: 10,
    titulo: "Troca de mouse"
);
```

Isso deixa os testes mais limpos, legíveis e fáceis de manter.

## Resumo da cobertura dos testes

Os testes implementados cobrem os seguintes pontos:

| Área                    | O que é validado                                                        |
| ----------------------- | ----------------------------------------------------------------------- |
| Regra de prioridade     | Verifica se chamados mais importantes são priorizados.                  |
| Heap/Fila de prioridade | Verifica enfileiramento, desenfileiramento, desempate e fila vazia.     |
| Validação de DTO        | Verifica campos obrigatórios e valores inválidos.                       |
| Criação de chamado      | Verifica se o chamado é enviado ao repositório.                         |
| Atualização de chamado  | Verifica se o chamado é atualizado com o ID correto.                    |
| Exclusão lógica         | Verifica exclusão de chamado existente e erro para chamado inexistente. |
| Controller              | Verifica respostas HTTP dos principais endpoints.                       |

## Importância dos testes no projeto

Os testes unitários ajudam a garantir que a API continue funcionando mesmo após alterações no código.

Eles são importantes porque:

* Evitam regressões.
* Validam regras de negócio.
* Documentam o comportamento esperado.
* Facilitam manutenção.
* Aumentam a confiabilidade do projeto.
* Permitem testar partes do sistema sem depender do banco de dados real.

No contexto deste projeto, os testes são especialmente importantes porque a regra de prioridade é o núcleo da aplicação. Garantir que a fila, o comparador e os casos de uso funcionem corretamente evita que chamados críticos sejam atendidos depois de chamados simples.

## Testes de API com arquivos JSON

Além dos testes unitários automatizados, o projeto também pode ser testado manualmente por meio de arquivos JSON utilizados em ferramentas como Swagger, Postman, Insomnia, curl ou arquivos `.http`.

Esses testes servem para validar os endpoints da API simulando requisições reais de criação e atualização de registros.

## Estrutura sugerida para testes manuais

Os arquivos JSON podem ser organizados em uma pasta chamada `Tests`, separada por entidade:

```text
Tests/
├── Categorias/
│   ├── POST_Categoria_1.json
│   ├── POST_Categoria_2.json
│   ├── PUT_Categoria_1.json
│   └── PUT_Categoria_2.json
│
├── Prioridades/
│   ├── POST_Prioridade_1.json
│   ├── POST_Prioridade_2.json
│   ├── PUT_Prioridade_1.json
│   └── PUT_Prioridade_2.json
│
├── Atendentes/
│   ├── POST_Atendente_1.json
│   ├── POST_Atendente_2.json
│   ├── PUT_Atendente_1.json
│   └── PUT_Atendente_2.json
│
├── Solicitantes/
│   ├── POST_Solicitante_1.json
│   ├── POST_Solicitante_2.json
│   ├── PUT_Solicitante_1.json
│   └── PUT_Solicitante_2.json
│
└── README.md
```

Essa estrutura ajuda a manter os exemplos de requisição organizados e facilita os testes durante a apresentação ou validação da API.

## URL base da API

Quando o projeto estiver rodando via Docker, a URL base será:

```text
http://localhost:8080
```

Caso a API seja executada localmente pelo Visual Studio ou `dotnet run`, a porta pode variar conforme o arquivo `launchSettings.json`.

Exemplo com Docker:

```text
http://localhost:8080/swagger
```

## Endpoints para testes manuais

### Chamados

```http
GET /api/Chamados/chamados-ti?pageNumber=1&pageSize=10
GET /api/Chamados/chamados-ti/{id}
POST /api/Chamados/chamados-ti
PUT /api/Chamados/chamados-ti/{id}
DELETE /api/Chamados/chamados-ti/{id}
GET /api/Chamados/chamados-ti/buscar?cpf=12345678900
GET /api/Chamados/chamados-ti/buscar?descricao=servidor
GET /api/Chamados/chamados-ti/proximo
POST /api/Chamados/chamados-ti/proximo/atender?id=1
GET /api/Chamados/chamados-ti/ordenados
```

### Categorias

```http
POST /api/chamado-ti/categorias
PUT /api/chamado-ti/categorias/{id}
```

### Prioridades

```http
POST /api/chamado-ti/prioridades
PUT /api/chamado-ti/prioridades/{id}
```

### Atendentes

```http
POST /api/chamado-ti/atendentes
PUT /api/chamado-ti/atendentes/{id}
```

### Solicitantes

```http
POST /api/chamado-ti/solicitantes
PUT /api/chamado-ti/solicitantes/{id}
```

Observação: caso algum desses endpoints auxiliares não esteja implementado no controller atual, os dados iniciais de categorias, prioridades, solicitantes e atendentes podem ser criados via seed automático no `Program.cs` ou diretamente no banco de dados.

## Modelos de JSON por entidade

### ChamadoDTO

Exemplo para criação de chamado:

```json
{
  "titulo": "Servidor principal caiu",
  "descricao": "Problema crítico afetando toda a empresa.",
  "CategoriaId": 1,
  "prioridadeId": 1,
  "status": 1,
  "SolicitanteId": 1,
  "atendente": null
}
```

Campos:

```text
titulo        → Título do chamado. Obrigatório.
descricao     → Descrição detalhada do chamado. Obrigatório e com no mínimo 10 caracteres.
CategoriaId   → ID da categoria cadastrada no banco.
prioridadeId  → ID da prioridade cadastrada no banco.
status        → Status do chamado conforme enum da aplicação.
SolicitanteId → ID do solicitante cadastrado no banco.
atendente     → Pode ser enviado como null na criação inicial.
```

### CategoriaDTO

```json
{
  "nome": "Suporte Técnico",
  "peso": 1
}
```

Campos:

```text
nome → Nome da categoria. Obrigatório.
peso → Peso usado na regra de prioridade. Obrigatório.
```

### PrioridadeDTO

```json
{
  "CategoriaId": 1,
  "descricao": "Alta",
  "peso": 10
}
```

Campos:

```text
CategoriaId → ID da categoria relacionada. Obrigatório.
descricao   → Descrição da prioridade. Obrigatório.
peso        → Peso usado no cálculo de prioridade. Obrigatório.
```

### AtendenteDTO

```json
{
  "nome": "Técnico Teste",
  "email": "tecnico.teste@empresa.com",
  "ativo": true
}
```

Campos:

```text
nome  → Nome do atendente. Obrigatório.
email → E-mail do atendente. Obrigatório.
ativo → Indica se o atendente está ativo ou inativo.
```

### SolicitanteDTO

```json
{
  "nome": "Usuário Teste",
  "email": "usuario.teste@empresa.com",
  "cpf": "12345678900",
  "ativo": true
}
```

Campos:

```text
nome  → Nome do solicitante. Obrigatório.
email → E-mail do solicitante. Obrigatório.
cpf   → CPF do solicitante. Obrigatório.
ativo → Indica se o solicitante está ativo ou inativo.
```

## Como testar via Swagger

Com a aplicação rodando, acesse:

```text
http://localhost:8080/swagger
```

Depois:

1. Escolha o endpoint desejado.
2. Clique em `Try it out`.
3. Preencha os parâmetros ou o body JSON.
4. Clique em `Execute`.
5. Verifique o código de resposta e o corpo retornado.

Exemplo de criação de chamado:

```json
{
  "titulo": "Servidor principal caiu",
  "descricao": "Problema crítico afetando toda a empresa.",
  "CategoriaId": 1,
  "prioridadeId": 1,
  "status": 1,
  "SolicitanteId": 1,
  "atendente": null
}
```

## Como testar via Postman ou Insomnia

1. Abra o Postman ou Insomnia.
2. Crie uma nova requisição.
3. Selecione o método HTTP desejado, como `POST`, `PUT`, `GET` ou `DELETE`.
4. Informe a URL do endpoint.
5. Para `POST` e `PUT`, vá até a aba `Body`.
6. Escolha a opção `raw`.
7. Selecione o formato `JSON`.
8. Cole o conteúdo do arquivo JSON correspondente.
9. Clique em `Send`.

Exemplo:

```http
POST http://localhost:8080/api/Chamados/chamados-ti
Content-Type: application/json
```

Body:

```json
{
  "titulo": "Servidor principal caiu",
  "descricao": "Problema crítico afetando toda a empresa.",
  "CategoriaId": 1,
  "prioridadeId": 1,
  "status": 1,
  "SolicitanteId": 1,
  "atendente": null
}
```

## Como testar via curl

### Criar chamado

```bash
curl -X POST http://localhost:8080/api/Chamados/chamados-ti \
  -H "Content-Type: application/json" \
  -d '{
    "titulo": "Servidor principal caiu",
    "descricao": "Problema crítico afetando toda a empresa.",
    "CategoriaId": 1,
    "prioridadeId": 1,
    "status": 1,
    "SolicitanteId": 1,
    "atendente": null
  }'
```

### Listar chamados

```bash
curl -X GET "http://localhost:8080/api/Chamados/chamados-ti?pageNumber=1&pageSize=10"
```

### Buscar chamado por ID

```bash
curl -X GET http://localhost:8080/api/Chamados/chamados-ti/1
```

### Atualizar chamado

```bash
curl -X PUT http://localhost:8080/api/Chamados/chamados-ti/1 \
  -H "Content-Type: application/json" \
  -d '{
    "titulo": "Servidor normalizado",
    "descricao": "Chamado atualizado após correção do problema.",
    "CategoriaId": 1,
    "prioridadeId": 1,
    "status": 2,
    "SolicitanteId": 1,
    "atendente": null
  }'
```

### Excluir chamado logicamente

```bash
curl -X DELETE http://localhost:8080/api/Chamados/chamados-ti/1
```

## Como testar via arquivo .http

Também é possível criar um arquivo `.http` dentro do projeto para executar requisições diretamente pelo Visual Studio ou VS Code.

Exemplo:

```http
POST http://localhost:8080/api/Chamados/chamados-ti
Content-Type: application/json

{
  "titulo": "Servidor principal caiu",
  "descricao": "Problema crítico afetando toda a empresa.",
  "CategoriaId": 1,
  "prioridadeId": 1,
  "status": 1,
  "SolicitanteId": 1,
  "atendente": null
}

###

GET http://localhost:8080/api/Chamados/chamados-ti?pageNumber=1&pageSize=10

###

GET http://localhost:8080/api/Chamados/chamados-ti/1

###

PUT http://localhost:8080/api/Chamados/chamados-ti/1
Content-Type: application/json

{
  "titulo": "Servidor normalizado",
  "descricao": "Chamado atualizado após correção do problema.",
  "CategoriaId": 1,
  "prioridadeId": 1,
  "status": 2,
  "SolicitanteId": 1,
  "atendente": null
}

###

DELETE http://localhost:8080/api/Chamados/chamados-ti/1
```

## Códigos de resposta esperados

| Método          | Código | Significado                                |
| --------------- | -----: | ------------------------------------------ |
| GET             |    200 | Requisição executada com sucesso.          |
| POST            |    201 | Registro criado com sucesso.               |
| POST            |    400 | Erro de validação ou dados inválidos.      |
| PUT             |    204 | Registro atualizado com sucesso.           |
| PUT             |    400 | Erro de validação ou dados inválidos.      |
| PUT             |    404 | Registro não encontrado.                   |
| DELETE          |    204 | Registro excluído logicamente com sucesso. |
| DELETE          |    404 | Registro não encontrado.                   |
| Qualquer método |    500 | Erro interno da aplicação.                 |

## Observações importantes para testes manuais

* Para criar um chamado, é necessário que existam registros válidos de categoria, prioridade e solicitante no banco.
* Se o banco estiver vazio, uma tentativa de criação pode gerar erro de chave estrangeira.
* Para evitar isso, o projeto pode utilizar seed automático no `Program.cs`, criando dados iniciais de categoria, prioridade, solicitante e atendente.
* No Docker, se quiser apagar o banco e recriar tudo do zero, execute:

```powershell
docker compose down -v
docker compose up -d --build
```

* O comando `docker compose down -v` remove os containers e também o volume do banco.
* Após subir novamente, as migrations são aplicadas automaticamente se o `Program.cs` estiver configurado com `Database.MigrateAsync()`.
* Para `PUT`, o ID deve ser informado na URL.
* Exemplo:

```http
PUT /api/Chamados/chamados-ti/1
```

* Os valores dos JSONs são exemplos e podem ser alterados conforme o cenário de teste.
* O CPF deve conter 11 dígitos.
* O e-mail deve estar em formato válido.
* Antes de testar, confirme que a API está rodando e que o Swagger abre corretamente.
