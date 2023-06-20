namespace DotLive.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string ReferenceMonthYear { get; private set; }
        public int EmployeeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime GeneratedAt { get; private set; }

        public Invoice(int id, string referenceMonthYear, int employeeId, decimal amount)
        {
            Id = id;
            ReferenceMonthYear = referenceMonthYear;
            EmployeeId = employeeId;
            Amount = amount;
            GeneratedAt = DateTime.Now;
        }
    }

}