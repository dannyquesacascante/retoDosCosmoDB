using retoDos.Application.CasosDeUso.Comando.CrearLog;
using retoDos.Application.CasosDeUso.Consulta.ConsultaLogsById;
using retoDos.Application.CasosDeUso.Consulta.ConsultarLogs;
using retoDos.Domain.Entidades;

namespace retoDos.Application.Repositorio
{
    public interface IRepositorioCosmos
    {
       Task <CrearLogComandoRespuesta> InsertLogAsync(Log log);
       Task<ConsultarLogsConsultaRespuesta> ConsultarLogs();
       Task<ConsultarLogsConsultaRespuestaById> ConsultarLogs(ConsultarLogsById request);

    }
}
