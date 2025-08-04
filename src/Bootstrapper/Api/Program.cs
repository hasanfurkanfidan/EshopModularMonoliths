using Catalog;
using Basket;
using Ordering;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration)
    ;

builder.Services.AddControllers();

var app = builder.Build();

app.UseCatalogModule()
   .UseBasketModule()
   .UseOrderingModule();

app.Run();
