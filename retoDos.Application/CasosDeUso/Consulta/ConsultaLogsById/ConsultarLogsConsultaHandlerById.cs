using MediatR;
using retoDos.Application.CasosDeUso.Consulta.ConsultarLogs;
using retoDos.Application.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retoDos.Application.CasosDeUso.Consulta.ConsultaLogsById
{
    public class ConsultarLogsConsultaHandlerById : IRequestHandler<ConsultarLogsById, ConsultarLogsConsultaRespuestaById>
    {
        IRepositorioCosmos _irep;
        public ConsultarLogsConsultaHandlerById(IRepositorioCosmos repo)
        {
            _irep = repo;
        }
 
        public async Task<ConsultarLogsConsultaRespuestaById> Handle(ConsultarLogsById request, CancellationToken cancellationToken)
        {
            ConsultarLogsConsultaRespuestaById ConsultaRespuesta = await _irep.ConsultarLogs(request);
            return ConsultaRespuesta;
        }
    }
}
