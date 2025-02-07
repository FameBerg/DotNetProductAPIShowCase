using System;

namespace DotNetProductAPIShowCase.Applications.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    { }
}
