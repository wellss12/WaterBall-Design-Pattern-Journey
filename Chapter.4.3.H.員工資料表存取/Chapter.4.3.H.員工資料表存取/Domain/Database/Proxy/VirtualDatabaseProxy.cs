using Chapter._4._3.H.員工資料表存取.Domain.Employee;

namespace Chapter._4._3.H.員工資料表存取.Domain.Database.Proxy;

public class VirtualDatabaseProxy : IDatabase
{
    private RealDatabase? _database;

    public IEmployee? GetEmployeeById(int id)
    {
        _database ??= new RealDatabase();
        return _database.GetEmployeeById(id);
    }
}