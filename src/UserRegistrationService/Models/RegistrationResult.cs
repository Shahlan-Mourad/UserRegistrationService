using System;

namespace UserRegistrationService.Models;

public class RegistrationResult
{
    public bool IsSuccess { get;}
    public string Message { get;}
    public string? Username { get;}

    public RegistrationResult(bool isSuccess, string username, string message)
    {
        IsSuccess = isSuccess;
        Username = username;
        Message = message;
    }
}
