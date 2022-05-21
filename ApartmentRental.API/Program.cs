using Microsoft.EntityFrameworkCore;
using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Repository;
using ApartmentRental.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MainContext>(options =>
    options.UseSqlite("DataSource=dbo.ApartmentRental.db",
        sqlOptions => sqlOptions.MigrationsAssembly("ApartmentRental.Infrastructure")
    )
);

builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddScoped<IApartmentService, ApartmentService>();
builder.Services.AddScoped<IAddressService, AddressService>();

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
