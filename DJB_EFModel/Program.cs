using DJB_Api;
using DJB_Application.Commands;
using DJB_Application.Queries;
using DJB_Core.Entities;
using DJB_Core.Options;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSwaggerGen();
builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<ExternalApiUrlsOptions>(builder.Configuration.GetSection("ExternalApiUrls"));

builder.Services.AddAppDI(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/GetExchangeRate/", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetExchangeRateQuery());
    return Results.Ok(result);

});

app.MapGet("/Products/", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetAllProductsQuery());
    return Results.Ok(result);

});

app.MapGet("/Products/{productId}", async (Guid productId, IMediator mediator) =>
{
    var result = await mediator.Send(new GetProductByIdQuery(productId));
    return Results.Ok(result);

});

app.MapPost("/Products/", async (ProductEntity product, IMediator mediator) =>
{
    var result = await mediator.Send(new AddProductCommand(product));
    return Results.Ok(result);

});

app.MapPut("/Products/{productId}", async (Guid productID, ProductEntity product, IMediator mediator) =>
{
    var result = await mediator.Send(new UpdateProductCommand(productID, product));
    return Results.Ok(result);

});

app.MapDelete("/Products/{productId}", async (Guid productID, IMediator mediator) =>
{
    var result = await mediator.Send(new DeleteProductCommand(productID));
    return Results.Ok(result);

});


app.Run();
