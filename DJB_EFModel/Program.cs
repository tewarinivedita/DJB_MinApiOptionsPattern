using DJB_Api;
using DJB_Application.Commands;
using DJB_Application.Queries;
using DJB_Core.Entities;
using DJB_Core.Options;
using DJB_Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStrings"));
    builder.Services.Configure<ExternalApiUrlsOptions>(builder.Configuration.GetSection("ExternalApiUrls"));
    builder.Services.AddDbContext<DataBaseContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("AZURE_SQL_CONNECTIONSTRING"));
    builder.Services.Configure<ExternalApiUrlsOptions>(builder.Configuration.GetSection("ExternalApiUrls"));
    builder.Services.AddDbContext<DataBaseContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));
}


builder.Services.AddAppDI(builder.Configuration);

var app = builder.Build();

//Created Github actions try pushing change new
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    
    app.UseSwaggerUI();
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

var productsGroup = app.MapGroup("/products")
    .WithTags("Products");

productsGroup.MapGet("/Products/", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetAllProductsQuery());
    return Results.Ok(result);

});

productsGroup.MapGet("/Products/{productId}", async (Guid productId, IMediator mediator) =>
{
    var result = await mediator.Send(new GetProductByIdQuery(productId));
    return Results.Ok(result);

});

productsGroup.MapPost("/Products/", async (ProductEntity product, IMediator mediator) =>
{
    var result = await mediator.Send(new AddProductCommand(product));
    return Results.Ok(result);

});

productsGroup.MapPut("/Products/{productId}", async (Guid productID, ProductEntity product, IMediator mediator) =>
{
    var result = await mediator.Send(new UpdateProductCommand(productID, product));
    return Results.Ok(result);

});

productsGroup.MapDelete("/Products/{productId}", async (Guid productID, IMediator mediator) =>
{
    var result = await mediator.Send(new DeleteProductCommand(productID));
    return Results.Ok(result);

});


app.Run();
