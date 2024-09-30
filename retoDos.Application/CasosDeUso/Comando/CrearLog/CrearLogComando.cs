using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;

namespace retoDos.Application.CasosDeUso.Comando.CrearLog
{
    public class CrearLogComando : IRequest<CrearLogComandoRespuesta>
    {
        public string logMessage { get; set; }


        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserLog Logtype { get; set; }

        public enum UserLog
        {
            Info,
            Warning,
            Error

        }

    }
}
