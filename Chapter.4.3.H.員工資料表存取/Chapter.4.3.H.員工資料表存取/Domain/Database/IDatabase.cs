using Chapter._4._3.H.員工資料表存取.Domain.Employee;

namespace Chapter._4._3.H.員工資料表存取.Domain.Database;

public interface IDatabase
{
    public IEmployee? GetEmployeeById(int id);
}