using AuctionService.Consumers;
using AuctionService.Data;
using AuctionService.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();

builder.Services.AddMassTransit(busConfig =>
{
    //Added cosumer for fault handling through which we will send acknowledgement to the producer through service bus (Rabbit bus) for any fault in Search Service DB
    busConfig.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();

    busConfig.AddEntityFrameworkOutbox<ApplicationDbContext>(outboxConfig =>
    {
        outboxConfig.QueryDelay = TimeSpan.FromSeconds(10);
        outboxConfig.UseSqlServer();
        outboxConfig.UseBusOutbox();
    });

    busConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("mybus-auction", false));

    busConfig.UsingRabbitMq((busContext, factoryConfig) =>
    {
        //factoryConfig.ReceiveEndpoint("auction-created", receivingConfig =>
        //{
        //    receivingConfig.UseMessageRetry(r => r.Interval(5, 5));
        //    receivingConfig.ConfigureConsumer<AuctionCreatedConsumer>(busContext);
        //});

        //_transport = new RabbitMQRegistertation(factoryConfig)
        //var bus = _transport.CreateBus();

        //It wil create topolgy such as queues, exchanges, bindings etc. based on the configuration provided in the consumer and endpoint configuration
        factoryConfig.ConfigureEndpoints(busContext);
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServiceBaseURL"];
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

DBInitializer.Initialize(app);

app.Run();
