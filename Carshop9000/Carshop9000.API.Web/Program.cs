using Carshop9000.Data.EfCore;
using Carshop9000.Model.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conString = "Server=(localdb)\\mssqllocaldb;Database=Carshop9000_Test;Trusted_Connection=true";
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>(x => new EfUnitOfWork(conString));

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
