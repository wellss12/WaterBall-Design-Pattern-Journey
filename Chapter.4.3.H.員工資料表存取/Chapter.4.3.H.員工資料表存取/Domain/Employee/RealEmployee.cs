namespace Chapter._4._3.H.員工資料表存取.Domain.Employee;

public class RealEmployee : IEmployee
{
    protected RealEmployee(int id, string name, int age)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id must be positive.");
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name must not be empty.");
        }

        Id = id;
        Name = name;
        Age = age;
    }

    public int Id { get; }
    
    public string Name { get; }
    
    public int Age { get; }

    public virtual List<IEmployee>? Subordinates { get; protected set; }
}