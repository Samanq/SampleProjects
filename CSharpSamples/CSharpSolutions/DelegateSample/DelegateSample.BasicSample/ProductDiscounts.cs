namespace DelegateSample.BasicSample;

public class ProductDiscounts
{
    public void ApplyBasicDiscount(Product product)
    {
        product.Price = product.Price / 2;
    }

    public void ApplyChristmasDiscount(Product product)
    {
        product.Price = product.Price / 3;
    }

    public void ApplyBlackFridayDiscount(Product product)
    {
        product.Price = product.Price / 5;
    }
}
