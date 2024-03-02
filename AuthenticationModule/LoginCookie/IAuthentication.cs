namespace SchoolTestsApp.AuthenticationModule.LoginCookie
{
    public interface ﻿IAuthentication
    {
        public Task Log_In(string username, string password, HttpContext context);

    }
}
