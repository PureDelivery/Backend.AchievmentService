using MassTransit;
using Microsoft.EntityFrameworkCore;
using PureDelivery.Achievments.API.Consumers;
using PureDelivery.Achievments.Application.EventHandler;
using PureDelivery.Achievments.Application.EventHandler.impl;
using PureDelivery.Achievments.Application.Repositories;
using PureDelivery.Achievments.Application.Services;
using PureDelivery.Achievments.Application.Services.impl;
using PureDelivery.Achievments.Infrastucture.Data;
using PureDelivery.Achievments.Infrastucture.Repositories;
using PureDelivery.Common.Configuration.Extensions;
using PureDelivery.Common.Configuration.Services;
using PureDelivery.Shared.Contracts.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurationProvider(builder.Configuration);

// Add services to the container.
builder.Services.AddDbContext<AchievmentsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<RabbitMqConfiguration>(sp =>
{
    var provider = sp.GetRequiredService<ICustomConfigurationProvider>();
    var cfg = provider.GetConfigurationAsync<RabbitMqConfiguration>("RabbitMQ").Result;
    cfg.Validate();
    return cfg;
});

// Регистрация логики ачивок
builder.Services.AddScoped<IEventHandler, EventsHandler>();
builder.Services.AddScoped<IAchievementService, AchievementService>();

// Регистрация репозиториев (без них хендлер не создастся)
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IFinishedAchievementsRepository, FinishedAchievementsRepository>();
builder.Services.AddScoped<IActiveAchievementsRepository, ActiveAchievementsRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<INotStartedAchievementsRepository, NotStartedAchievementsRepository>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedEventConsumer>();
    x.AddConsumer<OrderPaidEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitCfg = context.GetRequiredService<RabbitMqConfiguration>();
        cfg.Host(rabbitCfg.Host, rabbitCfg.VirtualHost, h =>
        {
            h.Username(rabbitCfg.Username);
            h.Password(rabbitCfg.Password);
        });
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
