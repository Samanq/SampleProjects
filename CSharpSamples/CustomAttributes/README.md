# Custom Attributes
We can use custom attributes to add **metadata** to types and members.<br>
This metadata can be retrieved at runtime using **reflection**.<br>
Custom attributes are useful when we want to provide additional information about our code to other parts of your application or external tools.

---

## Defining a custom attribute

To define a custom attribute, you need to create a new class that derives from the **System.Attribute** class.<br>
With [AttributeUsage] attribute we can specify to which objects we can apply this attribute.<br>
For the required properties we can add them to the **constructor**.

```C#
[AttributeUsage(AttributeTargets.Property)]
public class ReportItemAttribute : Attribute
{
    public ReportItemAttribute(string title)
    {
        Title = title;
    }
    
    public string Title { get; }

    public string Currency { get; set; } = string.Empty;
}

```

---

## Using a custom attribute

```C#
public class Product
{
    [ReportItem("Product Name")]
    public string Name { get; set; } = string.Empty;

    [ReportItem("Pice", Currency = "EUR")]
    public decimal Price { get; set; }
}

```

## Getting the value of custom property

```C#
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
            PropertyInfo? nameProperty = properties?.FirstOrDefault(p => p.Name == "Name"); // Getting the property
            PropertyInfo? pricePropery = properties?.FirstOrDefault(p => p.Name == "Price"); // Getting the property

            productTitle = nameProperty?.GetCustomAttribute<ReportItemAttribute>()?.Title; // Getting the value
            priceCurrency = pricePropery?.GetCustomAttribute<ReportItemAttribute>()?.Currency; // Getting the value
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
```