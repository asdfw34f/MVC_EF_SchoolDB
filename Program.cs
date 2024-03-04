using SchoolTestsApp.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using SchoolTestsApp.Repository.FilesManage;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<SchoolTestsApp.Repository.Authentication.ILogin, SchoolTestsApp.Repository.Authentication.Login>();
builder.Services.AddScoped<SchoolTestsApp.AuthenticationModule.LoginCookie.IAuthentication, SchoolTestsApp.AuthenticationModule.LoginCookie.Authentication>();
builder.Services.AddScoped<IFileManger, FileManager>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;
    var path = statusCodeContext.HttpContext.Request.Path;

    response.ContentType = "text/plain; charset=UTF-8";
    if (response.StatusCode == 403)
    {
        await response.WriteAsync($"Path: {path}. Access Denied ");
    }
    else if (response.StatusCode == 404)
    {
        await response.WriteAsync($"Resource {path} Not Found");
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorization}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "/",
    pattern: "{controller=Teacher}/{action=AddFile}/{id?}"
    );

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

app.Run();