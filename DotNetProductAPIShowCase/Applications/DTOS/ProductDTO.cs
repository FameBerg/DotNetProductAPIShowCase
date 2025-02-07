using System;

namespace DotNetProductAPIShowCase.Applications.DTOS;

public class ProductDTO
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
}
