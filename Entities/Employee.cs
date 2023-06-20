namespace DotLive.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string BusinessArea { get; set; }
        public decimal Salary { get; set; }
        public List<Invoice> Invoices {get; set;}

        public Employee(int id, string fullName, string businessArea, decimal salary, List<Invoice> invoices)
        {
            Id = id;
            FullName = fullName;
            BusinessArea = businessArea;
            Salary = salary;
            Invoices = invoices;
        }

    }

}