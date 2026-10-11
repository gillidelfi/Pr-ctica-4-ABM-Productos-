using Practica_CSR.Repositories.Implementations;
using Practica_CSR.Repositories.Interfaces;
using Practica_CSR.Services.Implementations;
using Practica_CSR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Practica_CSR.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<PracticaCSRContext>(options =>
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:PracticaCSRDBConnectionString"]));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();