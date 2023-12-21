# Delegate Sample
Delegate is a type that represents **references to methods** with a particular parameter list and return type.

## Defining a Delegate
```C#
public delegate void DiscountHandler(Product product);
```
Now our method can receive another **method** as a parameter
```C#
public class ProductService
{
    // Defining the delegate
    public delegate void DiscountHandler(Product product);

    // Accepting a method as parameter.
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

        // We are running the method here
        discountHandler(product);

        Console.WriteLine($"{product.Name} - ${product.Price}");
    }
}
```

## Calling a method with a delegate in parameters.
```C#
var discounts = new ProductDiscounts();

ProductService productService = new ProductService();

// We can use it lie this
productService.ApplyDiscount(discounts.ApplyBasicDiscount);
productService.ApplyDiscount(discounts.ApplyChristmasDiscount);

// Or like this
ProductService.DiscountHnadler discountHnadler = discounts.ApplyBasicDiscount;

discountHnadler += discounts.ApplyChristmasDiscount;
productService.ApplyDiscount(discountHnadler);

// Or like this
productService.ApplyDiscount(product => product.Price = product.Price / 10);
```