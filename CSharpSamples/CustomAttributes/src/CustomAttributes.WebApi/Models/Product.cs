using CustomAttributes.WebApi.Attributes;

namespace CustomAttributes.WebApi.Models;

public class Product
{
    [ReportItem("Product Name")]
    public string Name { get; set; } = string.Empty;

    [ReportItem("Pice", Currency = "EUR")]
    public decimal Price { get; set; }
}
