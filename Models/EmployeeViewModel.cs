public class EmployeeViewModel 
{
        public string FullName { get; set; }
        public string BusinessArea { get; set; }
        public decimal Salary { get; set; }

        public EmployeeViewModel(string fullName, string businessArea, decimal salary)
        {
            FullName = fullName;
            BusinessArea = businessArea;
            Salary = salary;
        }
}