namespace DelegateSample.BasicSample;

public class ProductService
{
    // Defining the delegate
    public delegate void DiscountHandler(Product product);

    public void ApplyDiscount(DiscountHandler discountHandler)
    {
        Product product = new Product()
        {
            Name = "Test",
            Price = 100m
        };


        // Instead of this
        //var discounts = new ProductDiscounts();

        //discounts.ApplyBasicDiscount(product);
        //discounts.ApplyChristmasDiscount(product);

        discountHandler(product);


        Console.WriteLine($"{product.Name} - ${product.Price}");
    }
}
