# Guia de Migrations - InternalHelpDeskApi

## O que foi criado

Foram criadas as seguintes migrations para o projeto:

### 1. **InitialCreate** (20240101000000_InitialCreate.cs)
   - Cria todas as tabelas do banco de dados:
     - `Atendentes` - Dados dos atendentes
     - `Solicitantes` - Dados dos solicitantes
     - `Prioridades` - Níveis de prioridade dos chamados
     - `Categorias` - Categorias de chamados (relacionadas com Prioridades)
     - `Chamados` - Registro de chamados (relacionados com Solicitantes, Atendentes e Categorias)

### 2. **DataBaseModelSnapshot.cs**
   - Snapshot do modelo de dados atual
   - Arquivo gerado automaticamente para controle de versão

## Como usar as migrations

### Aplicar as migrations ao banco de dados

Execute um dos comandos abaixo no Package Manager Console (PMC) ou CLI:

#### Opção 1: Package Manager Console (Visual Studio)
```powershell
Update-Database
```

#### Opção 2: .NET CLI (recomendado para .NET 8)
```bash
dotnet ef database update --project InternalHelpDeskApi.Infrastructure
```

### Criar nova migration após mudanças no modelo

Se você modificar as entidades (adicionar/remover propriedades), crie uma nova migration:

```bash
dotnet ef migrations add NomeDaMigration --project InternalHelpDeskApi.Infrastructure
```

Depois aplique:
```bash
dotnet ef database update --project InternalHelpDeskApi.Infrastructure
```

### Desfazer a última migration

```bash
Update-Database -Migration NomeDaMigrationAnterior
```

Ou com CLI:
```bash
dotnet ef database update NomeDaMigrationAnterior --project InternalHelpDeskApi.Infrastructure
```

## Configuração da Connection String

Certifique-se de que em `appsettings.json` está configurado:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(local);Database=InternalHelpDeskDb;Trusted_Connection=true;Encrypt=false;"
  }
}
```

Ou para SQL Server remoto:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=seu_servidor;Database=InternalHelpDeskDb;User Id=seu_usuario;Password=sua_senha;"
  }
}
```

## Estrutura do banco de dados

### Tabela: Atendentes
- Id (PK)
- Nome
- Email
- CriadoEm
- AtualizadoEm
- Ativo

### Tabela: Solicitantes
- Id (PK)
- Nome
- Email
- CPF (UK - Unique)
- CriadoEm
- AtualizadoEm
- Ativo

### Tabela: Prioridades
- Id (PK)
- CategoriaId
- Descricao
- Peso
- CriadoEm
- AtualizadoEm

### Tabela: Categorias
- Id (PK)
- Nome
- Peso
- CriadoEm
- AtualizadoEm
- PrioridadeId (FK)

### Tabela: Chamados
- Id (PK)
- Titulo
- Descricao
- CategoriaId (FK)
- Status (enum)
- SolicitanteId (FK)
- AtendenteId (FK)

## Próximas etapas

1. ? Migrations criadas
2. ? Injeção de dependência configurada
3. ? Aplicar migrations ao banco (`dotnet ef database update`)
4. ? Testar controllers e endpoints
5. ? Validar relacionamentos das entidades
