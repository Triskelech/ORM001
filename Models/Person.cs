namespace PruebasORM001.Models
{
    [Special]
    public class Person
    {
        public int Id { get; set; }

        [Special]
        public string? FirstName { get; set; }

        [Special]
        public string? LastName { get; set; }

        [Special]
        public IList<int>? Grades { get; set; } 

        public int Age { get; set; }
    }
}
