using MediatR;
using retoDos.Application.CasosDeUso.Comando.CrearLog;
using retoDos.Application.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using retoDos.Application.CasosDeUso.Consulta;

namespace retoDos.Application.CasosDeUso.Consulta.ConsultarLogs
{ 

    public class ConsultarLogsConsultaHandler : IRequestHandler<ConsultarLogs, ConsultarLogsConsultaRespuesta>
    {

        IRepositorioCosmos _irep;
        public ConsultarLogsConsultaHandler(IRepositorioCosmos repo)
        {
            _irep = repo;
        }

        public async Task<ConsultarLogsConsultaRespuesta> Handle(ConsultarLogs request, CancellationToken cancellationToken)
        {

            ConsultarLogsConsultaRespuesta ConsultaRespuesta = await _irep.ConsultarLogs();
            return ConsultaRespuesta;
        }
    }
}
