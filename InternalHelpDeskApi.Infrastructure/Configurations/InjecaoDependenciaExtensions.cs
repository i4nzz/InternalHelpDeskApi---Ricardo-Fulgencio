using global::InternalHelpDeskApi.Domain.Interfaces;
using global::InternalHelpDeskApi.Infrastructure.Persistence;
using global::InternalHelpDeskApi.Infrastructure.Repositories;
using InternalHelpDeskApi.Application;
using InternalHelpDeskApi.Application.Interfaces.UseCases;
using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Application.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternalHelpDeskApi.Infrastructure.Configurations
{
    public static class InjecaoDependenciaExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(connectionString))
            {
                services.AddDbContext<DataBase>(options => options.UseSqlServer(connectionString));
            }

            services.AddScoped<IChamadoRepository, ChamadosRepository>();
            services.AddScoped<IAtendenteRepository, AtendenteRepository>();

            services.AddScoped<ISoftDeleteChamadoUseCase, SoftDeleteChamadoUseCases>();
            services.AddScoped<IGetChamadoByIdUseCase, GetChamadoByIdUseCase>();
            services.AddScoped<IUpdateChamadoUseCase, UpdateChamadoUseCase>();
            services.AddScoped<IGetChamadoByDescUseCase, GetChamadosByDescUseCase>();
            services.AddScoped<IGetAllChamadosPagedUseCase, GetAllChamadosPagedUseCase>();
            services.AddScoped<ICriarChamadoUseCase, CriarChamadoUseCase>();
            services.AddScoped<IDistribuirChamadosUseCase, DistribuirProximoChamadoUseCase>();
            services.AddScoped<IGetChamadosByCPFSolicitanteUseCase, GetChamadosByCPFSolicitanteUseCase>();
            services.AddScoped<IObterListaDeChamadosOrdenadosUseCase, ObterListaDeChamadosOrdenadosUseCase>();

            return services;
        }
    }
}
