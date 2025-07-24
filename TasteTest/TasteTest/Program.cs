
using System.Globalization;
using TasteTest.Services;
using TasteTest.Utility;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IDbLayer, DbLayer>();  // Aggiunge DbLayer al container DI, altrimenti Build(); manda exception in shell

builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
builder.Services.AddScoped<ClientService>(); //addScoped --> diverso tipo di dependency injection, transient e ?? quante volte istanzio services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<DettService>();

builder.Services.AddScoped<IClientService, ClientService>(); // Registriamo l'interfaccia correttamente, altrimenti manda exception in view
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDettService, DettService>();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
//var configuration = new ConfigurationBuilder()
//            .AddJsonFile("appsettings.json")
//            .Build();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();





