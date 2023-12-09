using Chapter._4._3.H.員工資料表存取.Domain.Employee;

namespace Chapter._4._3.H.員工資料表存取.Domain.Database.Proxy;

public class ProtectionDatabaseProxy : VirtualDatabaseProxy
{
    public IEmployee? GetEmployeeById(int id)
    {
        var password = Environment.GetEnvironmentVariable("PASSWORD");
        if (password == "1qaz2wsx")
        {
            return base.GetEmployeeById(id);
        }

        throw new UnauthorizedAccessException();
    }
}