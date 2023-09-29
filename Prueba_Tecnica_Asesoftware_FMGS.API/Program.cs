using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_Asesoftware_FMGS.DA.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//COMANDO SCAFFOLD ENTITYFRAMEWORK
//Scaffold-DbContext "Server=DESKTOP-RTCGVG9\SQLEXPRESS01;Database=PruebaTecnica_Asesoftware_FMGS;User Id=sa;password=Fg_72345620;" Microsoft.EntityFrameworkCore.SqlServer -UseDatabaseNames -ContextDir Context -NoOnConfiguring -NoPluralize -OutputDir Entities -Context "AsesoftwareDbContext" -Tables dbo.comercios, dbo.servicios, dbo.turnos -f
// Add services to the container.

builder.Services.AddControllers();

//ignorando ciclos en respuesta json
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Obteniendo cadena de conexión
builder.Services.AddDbContext<AsesoftwareDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:AsesoftwareDB"]);
});

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
