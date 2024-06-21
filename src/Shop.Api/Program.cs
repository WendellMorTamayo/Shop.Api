using Microsoft.EntityFrameworkCore;
using Shop.Api.Data;
using Shop.Api.Endpoints;
using Shop.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataStoreContext>(options => options.UseSqlite(connString));

// 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapProductEndpoint();
app.MapCustomerEndpoint();
app.MapOrderEndpoint();

await app.MigrateDbAsync();

app.Run();