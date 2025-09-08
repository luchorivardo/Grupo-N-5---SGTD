using Data.Context;
using Data.Contracts;
using Data.Implementations;
using Data.Repositorios.Contracts;
using Data.Repositorios.Implementations;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Service.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRolService, RolService>();
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<IDisciplinaService, DisciplinaService>();
builder.Services.AddTransient<IEstadoService, EstadoService>();
builder.Services.AddTransient<IFacturaService, FacturaService>();
builder.Services.AddTransient<IProductoService, ProductoService>();
builder.Services.AddTransient<IProveedorService, ProveedorService>();
builder.Services.AddTransient<IRubroService, RubroService>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();

builder.Services.AddTransient<IRolRepository, RolRepository>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IDisciplinaRepository, DisciplinaRepository>();
builder.Services.AddTransient<IEstadoRepository, EstadoRepository>();
builder.Services.AddTransient<IFacturaRepository, FacturaRepository>();
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
builder.Services.AddTransient<IProveedorRepository, ProveedorRepository>();
builder.Services.AddTransient<IRubroRepository, RubroRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
