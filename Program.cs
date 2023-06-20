using DotLive.Entities;
using DotLive.Persistence;

WorkDbContext dbContext = new WorkDbContext();
List<Employee> dbEmployees = dbContext.Employees;


// FILTRAGEM:
List<Employee> itEmployees = dbEmployees.Where(e => e.BusinessArea == "TI").ToList();
List<Employee> superiorSalary = dbEmployees.Where(e => e.Salary >= 10_000m).ToList();
List<Employee> withoutInvoice = dbEmployees.Where(e => !e.Invoices.Any()).ToList();
//Métodos também podem ser usados para fazer checagens, veja: 
bool thereIsPoorEmployee = dbEmployees.Any(e => e.Salary < 1_000m);


// BUSCA:
//-> Single: retorna um único elemento que se encaixa com a expressão e se não houve ocorrerá uma exceção
//-> Trará exceção se houver mais de um elemento que satisfaça a condição (espera só um registro)
//Employee caio = dbEmployees.Single(x => x.FullName == "Caio");

//-> SingleOrDefault: evita que ocorre a exceção, pois agora se não encontrar irá trazer null
//-> Trará exceção se houver mais de um elemento que satisfaça a condição (espera só um registro)
//-> Sempre que possível use SingleOrDefault, pois evita exceções. 
Employee caio = dbEmployees.SingleOrDefault(e => e.FullName == "Caio");
Employee bruna = dbEmployees.SingleOrDefault(e => e.FullName == "Bruna");

//-> First: irá retornar sempre o primeiro elemento encontrado mesmo se houver mais que um elemento que satisfaça a condição;
//-> Diferentemente do Single, ele não gera exceção se houver mais de um elemento, pois sempre pegará o primeiro; 
//-> A exceção só ocorrerá se não satisfazer a condição, ou seja, retornar null
//Employee jonas = dbEmployees.First(e => e.FullName == "Jonas"); 

//FirstOrDefault: irá retornar o primeiro elemento que satisfaça a condição, caso contrário retornará nulo. 
Employee jonas = dbEmployees.FirstOrDefault(e => e.FullName == "Jonas");


// PROJEÇÃO: 
// É o ato de pegar um objeto e transformar em outro, ou seja, projetar. 
// Um outro exemplo seria pegar os funcionários e trazer somente o seu nome. 
// Podemos usar a projeção com instanciação também (ultimo exemplo). 
List<string> nomes = dbEmployees.Select(e => e.FullName).ToList();
List<EmployeeViewModel> employeesViewModel = dbEmployees.Select(e => new EmployeeViewModel(e.FullName, e.BusinessArea,e.Salary)).ToList();


// Também temos o método SelectMany para projeção. Permite criar uma lista com base em elementos/propriedades que são listas. 
// Permite fazer operações nas listagens das listagens. Ou seja, nas propriedades do tipo coleção que estão em uma lista de objetos.  
List<Invoice> allInvoices = dbEmployees.SelectMany(e => e.Invoices).ToList(); 


// ORDENAÇÃO: 
// Ordenando salário em ordem crescente
List<Employee> employeesSalaryAsc = dbEmployees.OrderBy(e => e.Salary).ToList();
// Ordenando salário em ordem decrescente
List<Employee> employeesSalaryDesc = dbEmployees.OrderByDescending(e => e.Salary).ToList();
// Ordenando por setor e depois por salário decrescente
List<Employee> employeesOrderByAreaThenBySalaryDesc = 
dbEmployees.OrderBy(e => e.BusinessArea)
.ThenByDescending(e => e.Salary).ToList();


// AGRUPAMENTO: 
// A ideia é similar ao group by do SQL, por exemplo. 
// Sempre que realizamos o agrupamento, a propriedade que estamos agrupando é a chave.

// Imagine querer a quantidade de funcionario de cada setor. 
var employeesCountByBusiness = 
dbEmployees.GroupBy(e => e.BusinessArea)
.Select(g => new {Department = g.Key, Count = g.Count()});

//Agora quero a quantidade de pessoas que ganham acima de 10K por área. 
var employeesOver10_000CountByArea = 
dbEmployees.GroupBy(e => e.BusinessArea)
.Select(g => new {Department = g.Key, Count = g.Count( e => e.Salary >= 10_000m)});




Console.ReadLine();