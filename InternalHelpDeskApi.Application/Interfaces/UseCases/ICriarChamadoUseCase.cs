using InternalHelpDeskApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalHelpDeskApi.Application.Interfaces.UseCases
{
    public interface ICriarChamadoUseCase
    {
        Task<ChamadosDtos> CriarChamado(ChamadosDtos novoChamado);
    }
}
