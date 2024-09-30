using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retoDos.Application.CasosDeUso.Consulta.ConsultaLogsById
{
    public class ConsultarLogsById : IRequest<ConsultarLogsConsultaRespuestaById>
    {
      public  int id { get; set; }

    }
}
