using retoDos.Application.Repositorio;
using retoDos.Domain.Entidades;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System;
using retoDos.Application.CasosDeUso.Comando.CrearLog;
using retoDos.Application.CasosDeUso.Consulta.ConsultarLogs;
using retoDos.Application.CasosDeUso.Consulta.ConsultaLogsById;


namespace retoDos.Infraestructure.Persistencia
{
    public class LogRepositorioCosmos : IRepositorioCosmos
    {
        private readonly Microsoft.Azure.Cosmos.Container _container;

        public LogRepositorioCosmos(Database database, IConfiguration configuration)
        {
            var containerName = configuration["CosmosDb:ContainerName"];
            _container = database.GetContainer(containerName);
        }

        public async Task<CrearLogComandoRespuesta> InsertLogAsync(Log log)
        {
            Console.WriteLine("ID->"+ log.id);
            Console.WriteLine("Message->"+ log.LogMessage);

            var response = await _container.CreateItemAsync(log);
            var isSuccess = response.StatusCode == System.Net.HttpStatusCode.Created;

           


            return new CrearLogComandoRespuesta
            {
                Id = log.id,
                EsCorrecto = isSuccess
            };
        }


        public async Task<ConsultarLogsConsultaRespuesta> ConsultarLogs()
        {

            // Definicion de la consulta
            var parameterizedQuery = new QueryDefinition(
                query: "SELECT * FROM logs p "
            );

            //Aquí se inicializa un FeedIterator que se utiliza para recorrer los resultados de la consulta.
            //CosmosDB devuelve los resultados en "páginas", y este iterador se encarga de manejar la paginación.
            using FeedIterator<Log> filteredFeed = _container.GetItemQueryIterator<Log>(
                queryDefinition: parameterizedQuery
            );
            

            ConsultarLogsConsultaRespuesta ConsultaRespuesta = new ConsultarLogsConsultaRespuesta();
            List<Log> lista = new List<Log>();

            
            while (filteredFeed.HasMoreResults)
            {
                try
                { //HasMoreResults se utiliza para verificar si hay más páginas de resultados disponibles.
                  //Si es así, se llama a ReadNextAsync() para obtener la siguiente página de resultados.
                    FeedResponse<Log> response =await  filteredFeed.ReadNextAsync();
                  
                    
                    lista.AddRange(response);// Se coloca esta linea en lugar del bucle.
               // foreach (Log item in response)
                //{
                        //lista.Add(item);



                 //}
                   ConsultaRespuesta.listaLogs = lista;
                }
                catch (Exception e)
                {

                    Console.WriteLine($"Found item:\t" + e.Message);
                }
            }

            return ConsultaRespuesta;

        }




        public async Task<ConsultarLogsConsultaRespuestaById> ConsultarLogs(ConsultarLogsById request)
        {

            // Build query definition
            var parameterizedQuery = new QueryDefinition(
                query: "SELECT * FROM logs p WHERE p.Logtype = @log"
            )
                .WithParameter("@log", request.id);

           
            using FeedIterator<Log> filteredFeed = _container.GetItemQueryIterator<Log>(
                queryDefinition: parameterizedQuery
            );
            Console.WriteLine(filteredFeed.ToString());
            ConsultarLogsConsultaRespuestaById n = new ConsultarLogsConsultaRespuestaById();
            List<Log> lista = new List<Log>();

        
            while (filteredFeed.HasMoreResults)
            {
                try
                {
                    FeedResponse<Log> response = await filteredFeed.ReadNextAsync();
                    lista.AddRange(response);            

                    // Iterate query results
                    // foreach (Log item in response)
                    //   {
                    //    Console.WriteLine($"Found item:" + item.id);
                    //    lista.Add(item);




                    //   }

                }
                catch (Exception e)
                {

                    Console.WriteLine($"Found item:\t" + e.Message);
                }
            }
            n.listaLogs = lista;
            return n;

        }

       
    }
}
