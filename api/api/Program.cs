using System.Text.Json.Serialization;
using api.Context;
using api.Domain.Repositories;
using api.Loaders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddDbContext<QuestionarioContext>(options =>
{
    options.UseInMemoryDatabase("Questionarios");
});
builder.Services.AddScoped<PerguntaRepository>()
    .AddScoped<PesquisaRepository>();
builder.Services.AddHostedService<PerguntaLoader>();

var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

