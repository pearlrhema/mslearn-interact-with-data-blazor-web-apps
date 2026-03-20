using BlazingPizza.Services;
// this statement allows the app to use the new service of the specified type, which is the database context we created in PizzaStoreContext.cs
using BlazingPizza.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// register the database context as a service, and specify that it should use an in-memory database
builder.Services.AddHttpClient();
builder.Services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db");

// register the OrderState service as a scoped service, which means that a new instance will be created for each user session
builder.Services.AddScoped<OrderState>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

app.Run();

app.Run();