namespace ObjectsRelationships
{
    public class Order
    {
        public IEnumerable<Food> Foods { get; }
        public DateTime DateTime { get; }

        // Composition relationship beetween Order and its food.
        // the foods cannot exist without Order.
        // When we delete the order, the foods that they are inside the order will be destroy.
        public Order(List<Food> foods)
        {
            Foods = foods;
            DateTime = DateTime.Now;
        }

        public double GetTotalPrice()
        {
            return Foods.Select(t => t.Price).Sum();
        }
    }
}
