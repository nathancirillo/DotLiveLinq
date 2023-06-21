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
List<EmployeeViewModel> employeesViewModel = dbEmployees.Select(e => new EmployeeViewModel(e.FullName, e.BusinessArea, e.Salary)).ToList();


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
.Select(g => new { Department = g.Key, Count = g.Count() });

//Agora quero a quantidade de pessoas que ganham acima de 10K por área. 
var employeesOver10_000CountByArea =
dbEmployees.GroupBy(e => e.BusinessArea)
.Select(g => new { Department = g.Key, Count = g.Count(e => e.Salary >= 10_000m) });


//Quantidade de funcionários com recibos emitidos 
var amoutOfInvoices = dbEmployees.GroupBy(e => e.Invoices.Count)
                                 .Select(g => new { NumberInvoices = g.Key, Employees = g.Select(e => e.FullName) })
                                 .ToList();


// JUNÇÃO (JOINS)
// A ideia será mostrar como funciona o inner join e o left join.

//INNER JOIN 
var innerJoin = dbEmployees.Join(allInvoices,
    employee => employee.Id, invoice => invoice.EmployeeId,
    (employee, invoice) => new { Name = employee.FullName, InvoiceAmount = invoice.Amount, InvoiceId = invoice.Id }).ToList();

var innerJoinQuerySintax = from employee in dbEmployees
                           join invoice in allInvoices
                           on employee.Id equals invoice.EmployeeId
                           select new
                           {
                               Name = employee.FullName,
                               InvoiceAmount = invoice.Amount,
                               InvoiceId = invoice.Id
                           };
var innerJoinQuerySintaxResult = innerJoinQuerySintax.ToList();


//LEFT JOIN
var leftJoin = dbEmployees.GroupJoin(allInvoices,
    employee => employee.Id, invoice => invoice.EmployeeId,
    (employee, invoices) => new
    {
        Name = employee.FullName,
        Invoices = invoices.ToList()
    }).ToList();



var leftJoinQuerySintax = from employee in dbEmployees
                          join invoice in allInvoices
                          on employee.Id equals invoice.EmployeeId into tempInvoices
                          select new 
                          {
                            Name = employee.FullName,
                            Invoices = tempInvoices.ToList()
                          };
var leftJoinQuerySintaxResult = leftJoinQuerySintax.ToList();


//PARTIÇÃO 
//consiste em pegar partes, por exemplo, a paginação. 
//existem dois métodos: skip e take 

//buscando somente três funcionários na ordem que está cadastrado
var threeEmployees = dbEmployees.Take(3).ToList();

//buscando os top 5 salários do empregados 
var topFiveSalaries = employeesSalaryDesc.Take(5).ToList();

//pulando os top 5 salários e pegando o restante
var allBut5TopSalaries = employeesSalaryDesc.Skip(5).ToList();

//aplicando paginação (exemplo):
var top5SalariesPaginated = Paginate(1,3);

IEnumerable<Employee> Paginate(int page, int pageSize)
{
    return dbEmployees.Skip((page - 1) * pageSize).Take(pageSize).ToList();
}


//AGREGAÇÃO 
// consiste em usar métodos que fazem agregação de informações, como por exemplo: calcular total, minímo, máximo, média, etc. 
var totalSalaries = dbEmployees.Sum(e => e.Salary);  
var averageSalaries = dbEmployees.Average(e => e.Salary);
var maxSalary = dbEmployees.Max(e => e.Salary); 
var minSalary = dbEmployees.Min(e => e.Salary);

//agora imagine que eu precise calcular a média salarial por setor. 
//estou misturando agrupamento, projeção e agregação.  
var averageSalaryByArea = dbEmployees.GroupBy(e => e.BusinessArea)
                                     .Select(g => new {Area = g.Key, AverageSalary = g.Average(e => e.Salary)})
                                     .ToList();


Console.ReadLine();