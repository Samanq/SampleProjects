namespace MappingSample
{
    public class Order
    {
        public IEnumerable<Food> Foods { get; set; }

        public Order(List<Food> foods)
        {
            Foods = foods;
        }

        public double GetTotalPrice()
        {
            return Foods.Select(t => t.Price).Sum();
        }
    }
}
