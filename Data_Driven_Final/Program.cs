using Data_Driven_Final.Models;
using Data_Driven_Final.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();             // Razor Pages
builder.Services.AddControllersWithViews();   // MVC (optional, but fine)

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<ProductService>();

builder.Services.AddHttpClient<GeminiService>();
builder.Services.Configure<GeminiOptions>(
builder.Configuration.GetSection("Gemini"));


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

app.MapRazorPages();

app.Run();
