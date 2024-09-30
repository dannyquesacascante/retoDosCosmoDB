using retoDos.Application.Common.Mapping;
using retoDos.Application.CasosDeUso;
using retoDos.Application.Repositorio;
using retoDos.Infraestructure.Persistencia;
using Microsoft.Azure.Cosmos;
using System.Text.Json.Serialization;
using retoDos.Application.CasosDeUso.Comando.CrearLog;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


// Registrar MediatR y AutoMapper
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CrearLogComandoHandler).Assembly));
builder.Services.AddAutoMapper(typeof(MappingProfile));  // Añadir AutoMapper y el Profile


// Configurar CosmosDB
builder.Services.AddSingleton(sp =>
{
    var cosmosClient = new CosmosClient(builder.Configuration["CosmosDb:ConnectionString"], builder.Configuration["CosmosDb:Key"]);
    return cosmosClient.GetDatabase(builder.Configuration["CosmosDb:DatabaseName"]);
});

// Registrar Repositorio
builder.Services.AddScoped<IRepositorioCosmos, LogRepositorioCosmos>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();








var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
