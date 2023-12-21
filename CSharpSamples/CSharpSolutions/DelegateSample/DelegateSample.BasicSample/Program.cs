using DelegateSample.BasicSample;

var discounts = new ProductDiscounts();

ProductService productService = new ProductService();

// We can use it lie this
//productService.ApplyDiscount(discounts.ApplyBasicDiscount);
//productService.ApplyDiscount(discounts.ApplyChristmasDiscount);

// Or like this
//ProductService.DiscountHnadler discountHnadler = discounts.ApplyBasicDiscount;
//discountHnadler += discounts.ApplyChristmasDiscount;
//productService.ApplyDiscount(discountHnadler);

// Or like this
productService.ApplyDiscount(product => product.Price = product.Price / 10);