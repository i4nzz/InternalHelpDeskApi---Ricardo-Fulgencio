using global::InternalHelpDeskApi.Domain.Interfaces;
using global::InternalHelpDeskApi.Infrastructure.Persistence;
using global::InternalHelpDeskApi.Infrastructure.Repositories;
using InternalHelpDeskApi.Application;
using InternalHelpDeskApi.Application.Interfaces;
using InternalHelpDeskApi.Application.UseCases;
using InternalHelpDeskApi.Applications;
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
                services.AddDbContext<HelpDeskContext>(options => options.UseSqlServer(connectionString));
            }

            services.AddScoped<IChamadoRepository, ChamadosRepository>();
            services.AddScoped<IAtendenteRepository, AtendenteRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IPrioridadeRepository, PrioridadeRepository>();
            services.AddScoped<ISolicitanteRepository, SolicitanteRepository>();

            services.AddScoped<ISoftDeleteChamadoUseCase, SoftDeleteChamadoUseCases>();
            services.AddScoped<IGetChamadoByIdUseCase, GetChamadoByIdUseCase>();
            services.AddScoped<IUpdateChamadoUseCase, UpdateChamadoUseCase>();
            services.AddScoped<IGetChamadoByDescUseCase, GetChamadosByDescUseCase>();
            services.AddScoped<IGetAllChamadosPagedUseCase, GetAllChamadosPagedUseCase>();
            services.AddScoped<ICriarChamadoUseCase, CriarChamadoUseCase>();
            services.AddScoped<IChamadosUrgentesUseCase, DistribuirProximoChamadoUseCase>();
            services.AddScoped<IGetChamadosByCPFSolicitanteUseCase, GetChamadosByCPFSolicitanteUseCase>();
            services.AddScoped<IObterListaDeChamadosOrdenadosUseCase, ObterListaDeChamadosOrdenadosUseCase>();

            services.AddScoped<IGetCategoriaByIdUseCase, GetCategoriaByIdUseCase>();
            services.AddScoped<IGetAllCategoriasPagedUseCase, GetAllCategoriasPagedUseCase>();
            services.AddScoped<ICriarCategoriaUseCase, CriarCategoriaUseCase>();
            services.AddScoped<IUpdateCategoriaUseCase, UpdateCategoriaUseCase>();
            services.AddScoped<IDeleteCategoriaUseCase, DeleteCategoriaUseCase>();

            services.AddScoped<IGetPrioridadeByIdUseCase, GetPrioridadeByIdUseCase>();
            services.AddScoped<IGetAllPrioridadesPagedUseCase, GetAllPrioridadesPagedUseCase>();
            services.AddScoped<ICriarPrioridadeUseCase, CriarPrioridadeUseCase>();
            services.AddScoped<IUpdatePrioridadeUseCase, UpdatePrioridadeUseCase>();
            services.AddScoped<IDeletePrioridadeUseCase, DeletePrioridadeUseCase>();

            services.AddScoped<IGetAtendenteByIdUseCase, GetAtendenteByIdUseCase>();
            services.AddScoped<IGetAllAtendentesPagedUseCase, GetAllAtendentesPagedUseCase>();
            services.AddScoped<ICriarAtendenteUseCase, CriarAtendenteUseCase>();
            services.AddScoped<IUpdateAtendenteUseCase, UpdateAtendenteUseCase>();
            services.AddScoped<IDeleteAtendenteUseCase, DeleteAtendenteUseCase>();

            services.AddScoped<IGetSolicitanteByIdUseCase, GetSolicitanteByIdUseCase>();
            services.AddScoped<IGetAllSolicitantesPagedUseCase, GetAllSolicitantesPagedUseCase>();
            services.AddScoped<ICriarSolicitanteUseCase, CriarSolicitanteUseCase>();
            services.AddScoped<IUpdateSolicitanteUseCase, UpdateSolicitanteUseCase>();
            services.AddScoped<IDeleteSolicitanteUseCase, DeleteSolicitanteUseCase>();

            return services;
        }
    }
}
