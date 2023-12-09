using Chapter._4._3.H.員工資料表存取.Domain.Employee;

namespace Chapter._4._3.H.員工資料表存取.Domain.Database;

public class RealDatabase : IDatabase
{
    private readonly Dictionary<int, IEmployee> _employeesMap;

    public RealDatabase()
    {
        _employeesMap = new Dictionary<int, IEmployee>
        {
            {1, new VirtualEmployeeProxy(1, "Alice", 20, this, 2, 3)},
            {2, new VirtualEmployeeProxy(2, "Bob", 30, this, 3)},
            {3, new VirtualEmployeeProxy(3, "Charlie", 40, this)},
        };
    }

    public IEmployee? GetEmployeeById(int id) =>
        _employeesMap.TryGetValue(id, out var employee) ? employee : null;
}