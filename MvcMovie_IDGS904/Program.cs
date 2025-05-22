using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie_IDGS904.Data;
using MvcMovie_IDGS904.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcMovie_IDGS904Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovie_IDGS904Context") ?? throw new InvalidOperationException("Connection string 'MvcMovie_IDGS904Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//PARA QUE NOS TOME DATOS AGREGAMOS LO SIGUIENTE
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
    
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
//aqui podemos especificar que pagina se abra por default en el proyecto
//por defalut es  pattern: "{controller=Home}/{action=Index}/{id?}")
// por default busca home y por default tambien busca index para iniciar, tmb se pasa un id
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
