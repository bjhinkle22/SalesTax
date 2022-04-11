using SalesTax.Domain.Interfaces;
using SalesTax.Domain.Services;
using System.Net.Http.Headers;
using SalesTax.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ITaxRateService, TaxRateService>();
builder.Services.AddHttpClient("TaxJarAPIBase", c =>
{
    c.BaseAddress = new Uri("https://api.taxjar.com/v2/");
    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5da2f821eee4035db4771edab942a4cc");
}
);

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
