using Carter;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Data;
using Shop.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

var connString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<DataStoreContext>(options => options.UseNpgsql(connString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();


app.Run();