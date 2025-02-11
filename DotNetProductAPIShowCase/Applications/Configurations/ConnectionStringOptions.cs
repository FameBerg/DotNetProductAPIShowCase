using System;

namespace DotNetProductAPIShowCase.Applications.Configurations;

//Map with variable in appsettings.json
public class ConnectionStringOptions
{
    public required string DefaultConnection { get; set; }
}
