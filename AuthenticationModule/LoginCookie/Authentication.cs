
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace SchoolTestsApp.AuthenticationModule.LoginCookie
{
    public class Authentication : IAuthentication
    {
        public Authentication() { }
        public async Task Log_In(string username, string password, HttpContext context)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            context.Response.Cookies.Append("username", username);
            context.Response.Cookies.Append("password", password);

        }
    }
}
