using DotLive.Entities;

namespace DotLive.Persistence
{
    public class WorkDbContext
    {
        public List<Employee> Employees {get; set;}
        public WorkDbContext()
        {
            Employees = new List<Employee>
            {
                new Employee(1, "Nathan", "TI", 10_000m, new List<Invoice>{ new Invoice(1, "06/2023",1,10_000m)}),
                new Employee(2, "Roberto","Marketing",12_000m,new List<Invoice>{new Invoice(2,"06/2023",2,12_000m)}),
                new Employee(3, "Thalles","Gestão",15_000m, new List<Invoice>{new Invoice(3,"06/2023",3,15_000m)}),
                new Employee(4, "Jaqueline","RH",2_530m, new List<Invoice>{new Invoice(4,"06/2023",4,2_530m)}),
                new Employee(5, "Fernanda","Support",3_892m, new List<Invoice>{new Invoice(5,"06/2023",5,3_892m)}),
                new Employee(6, "Luana","Sales",14_243m, new List<Invoice>{new Invoice(6,"06/2023",6,14_243m)}),
                new Employee(7, "Priscila", "TI", 8_240m, new List<Invoice>{new Invoice(7,"06/2023",7,8_240m)}),
                new Employee(8, "Matheus", "TI", 10_000m,new List<Invoice>{}),
                new Employee(9, "Bruna", "Atendimento", 1_785m,new List<Invoice>{new Invoice(9,"06/2023",9,1_785m)}),
                new Employee(10, "Lucas","TI",10_000m, new List<Invoice>{new Invoice(10,"06/2023",10,10_000m)})
            };
        }

    }

}