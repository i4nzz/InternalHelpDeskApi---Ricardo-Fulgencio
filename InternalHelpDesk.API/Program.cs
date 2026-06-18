using FluentValidation;
using FluentValidation.AspNetCore;
using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Infrastructure.Configurations;
using InternalHelpDeskApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ChamadosDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "InternalHelpDesk.API",
        Version = "v1",
        Description = "API para gerenciamento de chamados internos de TI, com cadastro, consulta, atualização, exclusão lógica e priorização de chamados."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

await AplicarMigrationsComRetry(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Em Docker, se a API estiver só em HTTP, essa linha pode atrapalhar.
// Se der problema de redirecionamento HTTPS, comente ela.
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task AplicarMigrationsComRetry(WebApplication app)
{
    const int quantidadeTentativas = 10;
    const int tempoEntreTentativasEmSegundos = 5;

    for (int tentativa = 1; tentativa <= quantidadeTentativas; tentativa++)
    {
        try
        {
            using var scope = app.Services.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<HelpDeskContext>();

            await db.Database.MigrateAsync();

            Console.WriteLine("Migrations aplicadas com sucesso.");

            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao aplicar migrations. Tentativa {tentativa}/{quantidadeTentativas}.");
            Console.WriteLine(ex.Message);

            if (tentativa == quantidadeTentativas)
            {
                throw;
            }

            await Task.Delay(TimeSpan.FromSeconds(tempoEntreTentativasEmSegundos));
        }
    }
}