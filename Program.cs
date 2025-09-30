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

app.MapPost("/pizzas", async (PizzaDB db, Pizza pizza) => 
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

app.MapGet("/pizzas/{id}", async (PizzaDB db, int id) => await db.Pizzas.FindAsync(id));

app.MapPut("/pizzas/{id}", async (PizzaDB db, Pizza updatepizza, int id) => 
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    pizza.Name = updatepizza.Name;
    pizza.Description = updatepizza.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/pizzas/{id}", async (PizzaDB db, int id) => 
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null)
    {
        return Results.NotFound();
    }

    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
