namespace ObjectsRelationships
{
    // Inheritance Relationship
    // Chef IS A Food
    public class Chef : Employee
    {
        // Aggregation Relationship
        // Chef HAS A OR SEVERAL Team members.
        public List<Employee> TeamMembers { get; set; } = new List<Employee>();
        public void Tech()
        {
            string membersName = string.Join(", ", TeamMembers.Select(t => $"{t.FirstName} {t.LastName} "));

            Console.WriteLine($"Chef {FirstName} {LastName} is teaching to {membersName}");
        }

        // Association relationshio beetween Chef and knife.
        // Chef object use a Knife object but Chef doesn't affect the knife life cycle.
        public void CutBread(Knife knife)
        {
            Console.WriteLine($"Chef {LastName} is cutting a bread with {knife.BladeSize} cm knife.");
        }
    }
}
