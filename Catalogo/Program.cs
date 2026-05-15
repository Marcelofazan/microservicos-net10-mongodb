using Catalogo.Entities;
using Microsoft.AspNetCore.OpenApi;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Pacote;
using Pacote.MongoDB;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//Configure GuidRepresentation
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

// Add services to the container.
builder.Services.AddControllers(
    options => options.SuppressAsyncSuffixInActionNames = false
);

// Registra o IMongoDatabase
builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("items");
});

builder.Services.AddMongo().AddMongoRepository<Item>("items");

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add swagger
builder.Services.AddEndpointsApiExplorer();

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
