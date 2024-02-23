using SchoolTestsApp.Models.DB;
using Microsoft.EntityFrameworkCore;
using SchoolTestsApp.Repository;
using SchoolTestsApp.Models.DB.Entities;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<SchoolTestsApp.Repository.LoginStudent.ILogin, SchoolTestsApp.Repository.LoginStudent.AuthenticateLogin>();
builder.Services.AddScoped<SchoolTestsApp.Repository.LoginTeacher.ILogin, SchoolTestsApp.Repository.LoginTeacher.AuthenticateLogin>();


// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Authorization}/{action=Index}/{id?}");

app.Run();
