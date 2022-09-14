using ObjectsRelationships;

Cook cook = new Cook
{
    FirstName = "Jane",
    LastName = "Doe"
};

Chef chef = new Chef
{
    FirstName = "John",
    LastName = "Doe",
    Salary = 80000,
    // Aggregation Relationship
    // If we delete the chef object, it doesn't affect the life cycle of cook object.
    TeamMembers = new List<Employee> { cook }
};

chef.Tech();


Knife knife = new Knife { BladeSize = 10 };

// Association relationship.
// Chef only use a knife object and it is not in charge of knife object.
chef.CutBread(knife);


Order order = new Order(new List<Food> { new ChesseBurger(), new ClassicBurger() });

Console.WriteLine($"total price of order is : {order.GetTotalPrice()}");