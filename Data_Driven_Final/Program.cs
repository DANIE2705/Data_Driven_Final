using Data_Driven_Final.Models;
using Data_Driven_Final.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();   // MVC
builder.Services.AddRazorPages();             // Razor Pages (secondary)
builder.Services.AddMvc();

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ⭐ Force MVC to take priority
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Products}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});

// ⭐ MVC FIRST
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

// ⭐ Razor Pages SECOND
app.MapRazorPages();

app.Run();
