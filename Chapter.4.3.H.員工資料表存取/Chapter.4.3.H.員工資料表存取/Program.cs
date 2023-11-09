using Chapter._4._3.H.員工資料表存取.Domain.Database.Proxy;

var protectionDatabaseProxy = new ProtectionDatabaseProxy();
var employee = protectionDatabaseProxy.GetEmployeeById(1);
var subordinates = employee!.Subordinates;
Console.WriteLine($"Employee: {employee.Name}");
Console.WriteLine($"Subordinates: {string.Join(", ", subordinates!.Select(x => x.Name))}");