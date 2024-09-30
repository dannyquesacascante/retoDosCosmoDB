using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using retoDos.Domain.Entidades;
using retoDos.Application.CasosDeUso.Comando.CrearLog;
namespace retoDos.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<CrearLogComando, Log > ();
          
        }
          
           
    }
}
