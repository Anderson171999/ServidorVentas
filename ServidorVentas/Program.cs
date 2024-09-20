using Microsoft.EntityFrameworkCore;
using SistemaVenta.Server.Models;
using SistemaVenta.Server.Repositorio.Contrato;
using SistemaVenta.Server.Utilidades;
using SistemaVentaBlazor.Server.Repositorio.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión al DbContext
builder.Services.AddDbContext<DBVentaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

//MOBIL
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
//END MOBIL

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IEventoRepositorio, EventoRepositorio>();



// Añadir servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//MOBIL
// Usar CORS en el pipeline
app.UseCors("AllowAll");
//END MOBIL


// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting(); // Asegura que se habilite el enrutamiento

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Aquí se asignan los controladores a las rutas
});

app.Run();
