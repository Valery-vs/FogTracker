﻿namespace FogTracker.Contracts.Services
{
    public interface IAuthService
    {
        string Authenticate(string username, string password);
    }
}