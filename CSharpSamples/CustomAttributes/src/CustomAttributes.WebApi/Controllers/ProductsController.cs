using CustomAttributes.WebApi.Attributes;
using CustomAttributes.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CustomAttributes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = new List<Product> 
            {
                new Product{Name ="Apple", Price = 12},
                new Product{Name ="Orange", Price = 15},
                new Product{Name ="Peach", Price = 17},
            };

            IEnumerable<ProductPrintModel> printableProducts = GetPrintableProducts(products);

            return Ok(printableProducts);
        }

        private IEnumerable<ProductPrintModel> GetPrintableProducts(IEnumerable<Product> products)
        {
            var printableProducts = new List<ProductPrintModel>();

            var properties = typeof(Product).GetProperties();

            foreach (var product in products)
            {
                string? productTitle = string.Empty;
                string? priceCurrency = string.Empty;

                if (properties is not null)
                {
                    PropertyInfo? nameProperty = properties?.FirstOrDefault(p => p.Name == "Name");
                    PropertyInfo? pricePropery = properties?.FirstOrDefault(p => p.Name == "Price");

                    productTitle = nameProperty?.GetCustomAttribute<ReportItemAttribute>()?.Title;
                    priceCurrency = pricePropery?.GetCustomAttribute<ReportItemAttribute>()?.Currency;
                }


                printableProducts.Add(
                    new ProductPrintModel 
                    { 
                        Name = $"{productTitle} : {product.Name}",
                        Price = $"{product.Price} {priceCurrency}" 
                    });
                
            }

            return printableProducts;
        }
    }
}
