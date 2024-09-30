using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using retoDos.Application.CasosDeUso.Consulta.ConsultarLogs;
using retoDos.Domain.Entidades;
using Swashbuckle.AspNetCore.Annotations;
using retoDos.Application.CasosDeUso.Comando.CrearLog;
using retoDos.Application.CasosDeUso.Consulta.ConsultaLogsById;

namespace retoDosCosmoDB.Controllers
{

    public class LogController : Controller
    {
        private readonly IMediator _mediator;

        public LogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/log/crearlog")]
        public async Task<IActionResult> CreateLog([FromBody] CrearLogComando command)

        {

            if (!ModelState.IsValid)
            {

                // Verificamos si el error es de conversión de Enum
                if (ModelState.ContainsKey("$.logtype"))
                {
                   var  errorMessage = "El valor proporcionado para 'Logtype' no es válido. Valores permitidos: 'Info', 'Warning', 'Error'.";
                    return BadRequest(new { message = errorMessage });

                }

                  return BadRequest(ModelState);
            }

            var result = await _mediator.Send(command);

            if (result.EsCorrecto)
            {
                return Ok(new { Id = result.Id, EsCorrecto = result.EsCorrecto });
            }
            else
            {
                return StatusCode(500, new { Id = result.Id, EsCorrecto = result.EsCorrecto });
            }
        }

        [HttpGet("api/log/consultalogs")]
        public async Task<IActionResult>  ConsultarLog()

        {

            var consulta = new ConsultarLogs();

            var result = await  _mediator.Send(consulta);

            return Ok(new { Logs=result.listaLogs }); ;



        }

      

        [HttpGet("api/log/consultalogs/{id}")]
        public async Task<IActionResult> ConsultarLog( CrearLogComando.UserLog id)

        {

            ConsultarLogsById  consultarLogsById = new ConsultarLogsById();
            consultarLogsById.id = (int)id;

            var result = await _mediator.Send(consultarLogsById);

            return Ok(new { Logs = result.listaLogs }); ;



        }



    }
}
