using MappingSample;

// Generating sample object.
var objects = new[]
{
    new{ first_name = "John", last_name = "Doe", person_age = 20},
    new{ first_name = "Jane", last_name = "Doe", person_age = 21},
    new{ first_name = "Peter", last_name = "Jackson", person_age = 21},
    new{ first_name = "James", last_name = "Cameron", person_age = 21}
};

// Mapping the object to studenst
var students = StudentMapper.MapToStudent(objects);


// Printing the result.
foreach (var item in students)
{
    Console.WriteLine($"{item.FirstName} {item.LastName} {item.Age} years old.");
}



Food burger = new Food { Price = 10.2, Title = "Burger"};
Food pizza = new Food { Price = 14.5, Title = "Pizza" };

Order order = new Order(new List<Food>() {burger, pizza });

Console.WriteLine($"\nOrder total price is {order.GetTotalPrice()}\n");