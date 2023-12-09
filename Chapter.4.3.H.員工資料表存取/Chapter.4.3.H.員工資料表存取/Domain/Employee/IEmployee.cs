namespace Chapter._4._3.H.員工資料表存取.Domain.Employee;

public interface IEmployee
{
    public int Id { get; }
    public string Name { get; }
    public int Age { get; }
    public List<IEmployee>? Subordinates { get; }
}