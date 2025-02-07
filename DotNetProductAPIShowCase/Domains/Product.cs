using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetProductAPIShowCase.Domains;

// NOTE: Bind in Domain class is easy and quick but it make dependency with the DB adapter
[Table("Products")]
public class Product
{
    public int Id {get; set;}

    public required string Name {get; set;}

    public decimal Price {get; set;}

    public string? Description {get; set;}
}
