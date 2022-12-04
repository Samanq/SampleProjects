using BlazorSample.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSample.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = new List<Product>
    {
        new Product
        {
            Id = 1,
            Title = "Iphone 14",
            Description = "apple smart phone",
            ImageUrl = "https://www.apple.com/newsroom/images/product/iphone/geo/Apple-iPhone-14-iPhone-14-Plus-hero-220907-geo.jpg.og.jpg?202211022245",
            Price = 25.6m
        },
        new Product
        {
            Id = 2,
            Title = "Samsung S22 ultra",
            Description = "samsung smart phone",
            ImageUrl = "https://images.samsung.com/hr/smartphones/galaxy-s22-ultra/buy/02_carousel/04_basic-colors/s22_ultra_productkv_phantomblack_mo.jpg",
            Price = 20.6m
        },
        new Product
        {
            Id = 3,
            Title = "Samsung S21",
            Description = "Samsung old smart phone",
            ImageUrl = "https://images.samsung.com/hr/smartphones/galaxy-s21/buy/02_ImageCarousel/02_GroupShot-P3/S21_Ultra_BasicGroup_KV_MO_img.jpg",
            Price = 18.6m
        }
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_products);
    }
}
