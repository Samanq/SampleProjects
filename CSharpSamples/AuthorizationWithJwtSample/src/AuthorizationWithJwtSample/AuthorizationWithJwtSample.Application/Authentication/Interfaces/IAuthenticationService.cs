﻿namespace AuthorizationWithJwtSample.Application.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string name, string email, string password);
        AuthenticationResult Login(string email, string password);
    }
}