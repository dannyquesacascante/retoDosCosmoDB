using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using retoDos.Application.Repositorio;
using AutoMapper;
using retoDos.Domain.Entidades;

namespace retoDos.Application.CasosDeUso.Comando.CrearLog
{
    public class CrearLogComandoHandler : IRequestHandler<CrearLogComando, CrearLogComandoRespuesta>
    {

        private readonly IRepositorioCosmos _irepo;
        private readonly IMapper _mapper;

        public CrearLogComandoHandler(IRepositorioCosmos irepo, IMapper mapper)
        {
            _irepo = irepo;
            _mapper = mapper;
        }

        public async Task<CrearLogComandoRespuesta> Handle(CrearLogComando request, CancellationToken cancellationToken)
        {


            var log = _mapper.Map<Log>(request);

            var crearLogComandoRespuesta = await _irepo.InsertLogAsync(log);

            return crearLogComandoRespuesta;
        }










    }
}
