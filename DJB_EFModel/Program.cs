using DJB_Api;
using DJB_Api.Middleware;
using DJB_Application.Commands;
using DJB_Application.Dto;
using DJB_Application.Queries;
using DJB_Core.Entities;
using DJB_Core.Interfaces;
using DJB_Infrastructure.Data;
using DJB_Infrastructure.Http;
using DJB_Infrastructure.Repository;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<CorrelationIdHandler>();
builder.Services
    .AddHttpClient<IExternalVendorRepository, ExternalVendorRepository>()
    .AddHttpMessageHandler<CorrelationIdHandler>();
builder.Services
    .AddHttpClient("DownstreamApi", client =>
    {
        client.BaseAddress = new Uri("https://localhost:7119");
    })
    .AddHttpMessageHandler<CorrelationIdHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSwaggerGen(); 
builder.Services.AddCors();
builder.Services.AddAppDI(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<CorrelationIdMiddleware>();



//Created Github actions try pushing change new
app.UseSwagger();
app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7235")
          .AllowAnyMethod()
          .AllowAnyHeader()
          );

if (app.Environment.IsDevelopment())
{
    
    app.UseSwaggerUI();
    var application = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataBaseContext>();

    //USe this to apply pending migrations automatically when the application starts
    //var pendingMigrations = await application.Database.GetPendingMigrationsAsync();
    //if (pendingMigrations != null)
    //    await application.Database.MigrateAsync();
}
else
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("../swagger/v1/swagger.json", "DJB API V1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();
#region AI
var AIGroup = app.MapGroup("api/AI").WithTags("AI");

AIGroup.MapPost("/Chat/", async (ChatRequest chatRequest, IMediator mediator) =>
{
    var result = await mediator.Send(new AskAnalyticsQuery(chatRequest.Message));
    return Results.Ok(result);
});
AIGroup.MapPost("/AskAnything/", async (ChatRequest chatRequest, IMediator mediator) =>
{
    var result = await mediator.Send(new AskAnyThingAIQuery(chatRequest.Message));
    return Results.Ok(result);
});
#endregion

#region Apiexternal
var externalGroup = app.MapGroup("/External")
    .WithTags("External");

externalGroup.MapGet("/GetPokemon/", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetPokemonQuery());
    return Results.Ok(result);

});

externalGroup.MapGet("/GetJokes/", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetJokeQuery());
    return Results.Ok(result);

});
#endregion Apiexternal

#region Internal
var productsGroup = app.MapGroup("/products")
    .WithTags("Products");

productsGroup.MapGet("/", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetAllProductsQuery());
    return Results.Ok(result);

});

productsGroup.MapGet("/{productId}", async (Guid productId, IMediator mediator) =>
{
    var result = await mediator.Send(new GetProductByIdQuery(productId));
    return Results.Ok(result);

});

productsGroup.MapPost("/", async (ProductEntity product, IMediator mediator) =>
{
    var result = await mediator.Send(new AddProductCommand(product));
    return Results.Ok(result);

});

productsGroup.MapPut("/{productId}", async (Guid productID, ProductEntity product, IMediator mediator) =>
{
    var result = await mediator.Send(new UpdateProductCommand(productID, product));
    return Results.Ok(result);

});

productsGroup.MapDelete("/{productId}", async (Guid productID, IMediator mediator) =>
{
    var result = await mediator.Send(new DeleteProductCommand(productID));
    return Results.Ok(result);

});

var ordersGroup = app.MapGroup("/Orders")
    .WithTags("Orders");
var ordersUrl = "/Orders/"; 

ordersGroup.MapGet(ordersUrl, async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetAllOrdersQuery());
    return Results.Ok(result);

});

ordersGroup.MapGet(ordersUrl + "{orderId}", async (Guid orderId, IMediator mediator) =>
{
    var result = await mediator.Send(new GetOrdersByIdQuery(orderId));
    return Results.Ok(result);

});

ordersGroup.MapPost(ordersUrl, async (OrderEntity order, IMediator mediator) =>
{
    var result = await mediator.Send(new AddOrderCommand(order));
    return Results.Ok(result);

});

ordersGroup.MapPut(ordersUrl + "{orderId}", async (Guid orderId, OrderEntity order, IMediator mediator) =>
{
    var result = await mediator.Send(new UpdateOrderCommand(orderId, order));
    return Results.Ok(result);

});

ordersGroup.MapDelete(ordersUrl + "{orderId}", async (Guid orderId, IMediator mediator) =>
{
    var result = await mediator.Send(new DeleteOrderCommand(orderId));
    return Results.Ok(result);

});
#endregion Internal


//test propagation of correlation id - Open DownstreamApi and run before testing this
app.MapGet("/test-propagation", async (
    IHttpClientFactory httpClientFactory,
    HttpContext context) =>
{
    var outgoingId = context.Request.Headers["X-Correlation-ID"]
        .FirstOrDefault();

    var client = httpClientFactory.CreateClient("DownstreamApi");
    var downstream = await client.GetFromJsonAsync<DownstreamResponse>(
        "/test");

    return Results.Ok(new
    {
        Service = "DJB_Api",
        OutgoingCorrelationId = outgoingId,
        Downstream = downstream
    });
});




app.Run();
public sealed record DownstreamResponse(
    string? Service,
    string? ReceivedCorrelationId);
