using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication3;

using WebApplication3.Models;
using WebApplication3.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Shopping_cartContext>(c =>
c.UseSqlServer(connectionString));

 builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<Shopping_cartContext>()
        .AddDefaultTokenProviders();

builder.Services.AddTransient<IHomeRepository, Homerepository>();
builder.Services.AddTransient<IcartRepository, CartRepository>();
builder.Services.AddTransient<IOrdrUserRepo, OrderUserRepo>();


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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
using var scope = app.Services.CreateScope();


app.Run();

app.Run();
