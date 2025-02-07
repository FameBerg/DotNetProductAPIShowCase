using System;

namespace DotNetProductAPIShowCase.Applications.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    { }
}
