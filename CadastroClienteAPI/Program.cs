using CadastroClienteAPI.Infrastructure;
using CadastroClienteAPI.Infrastructure.Mappings.YourNamespace.Infrastructure.Mappings;
using CadastroClienteAPI.Interface;
using CadastroClienteAPI.Models.Enum;
using CadastroClienteAPI.Repositories;
using CadastroClienteAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();

// Registrando o repositório
//builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.EnableAnnotations();

    // Configurar a descrição dos enums
    options.MapType<Enum>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = (IList<IOpenApiAny>)Enum.GetValues(typeof(StatusCliente)).Cast<Enum>()
                     .Select(e => new OpenApiString(e.GetEnumDescription()))
                     .ToList()
    });
});

//builder.Services.AddHttpClient<BuscaEnderecoCepService>();

builder.Services.AddDbContext<CadastroClienteContext>(options =>
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("CadastroCliente")));

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddControllers();
builder.Services.AddAuthorization(); // Necessário para políticas de autorização

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
