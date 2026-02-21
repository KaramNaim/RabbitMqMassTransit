using MassTransit;
using Consumer.Worker.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<NotificationRequestedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbit = builder.Configuration.GetSection("RabbitMq");
        cfg.Host(rabbit["Host"], rabbit["VirtualHost"], h =>
        {
            h.Username(rabbit["Username"]!);
            h.Password(rabbit["Password"]!);
        });

        // Create an endpoint (queue) for consumers automatically
        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();