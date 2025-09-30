using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<PizzaDB>(options => options.UseInMemoryDatabase("items"));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo {
            Title = "PizzaStore API",
            Description = "Keep track of your pizzas",
            Version = "v1" });
    });

}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
    });
}

app.MapGet("/pizzas", async (PizzaDB db) => await db.Pizzas.ToListAsync());

// app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
// app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
// app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
// app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));

app.Run();
