using Inventario;
using Inventario.Clients;
using Inventario.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Pacote;
using Pacote.MongoDB;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

BsonSerializer.RegisterSerializationProvider(new GuidSerializationProvider());


builder.Services.AddMongo().AddMongoRepository<InventoryItem>("InventoryItems");

builder.Services.AddControllers(
    options => options.SuppressAsyncSuffixInActionNames = false
);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient<CatalogClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7026/");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    options.SwaggerEndpoint("/openapi/v1.json", "OpenAI Agent API V1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();