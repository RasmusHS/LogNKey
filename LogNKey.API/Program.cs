using Application;
using Application.Passwords;
using Application.Passwords.Create;
using Carter;
using Persistence;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using LogNKey.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCarter();

builder.Services.AddRebus(rebus => rebus
    .Routing(r =>
        r.TypeBased().MapAssemblyOf<ApplicationAssemblyReference>("lognkey-queue"))
    .Transport(t =>
        t.UseRabbitMq(
            builder.Configuration.GetConnectionString("MessageBroker"),
            "lognkey-queue"))
    .Sagas(s =>
        s.StoreInPostgres(
            builder.Configuration.GetConnectionString("Database"),
            "sagas",
            "saga_indexes")),
    onCreated: async bus =>
    {
        await bus.Subscribe<GeneratePasswordEvent>();
        await bus.Subscribe<GeneratedPasswordChecked>();
    });

builder.Services.AutoRegisterHandlersFromAssemblyOf<ApplicationAssemblyReference>();

builder.Services.AddHttpClient<CheckGeneratedPasswordHandler>(
    client => client.BaseAddress = new Uri(builder.Configuration["AI"]));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

//app.UseSwagger();
//app.UseSwaggerUI();
//app.ApplyMigrations();

//app.UseHttpsRedirection();

app.MapCarter();

app.Run();
