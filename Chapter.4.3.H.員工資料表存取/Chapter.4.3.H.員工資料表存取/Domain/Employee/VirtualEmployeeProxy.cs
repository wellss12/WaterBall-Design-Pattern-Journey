using Chapter._4._3.H.員工資料表存取.Domain.Database;

namespace Chapter._4._3.H.員工資料表存取.Domain.Employee;

public class VirtualEmployeeProxy : RealEmployee
{
    public VirtualEmployeeProxy(int id, string name, int age, IDatabase database, params int[] subordinateIds)
        : base(id, name, age)
    {
        _database = database;
        _subordinateIds = subordinateIds;
    }

    private readonly IDatabase _database;
    private readonly IEnumerable<int> _subordinateIds;

    public override List<IEmployee>? Subordinates
    {
        get
        {
            LazyLoad();
            return base.Subordinates;
        }
        protected set => base.Subordinates = value;
    }

    private void LazyLoad()
    {
        if (base.Subordinates is null && _subordinateIds.Any())
        {
            base.Subordinates = _subordinateIds
                .Select(subordinateId => _database.GetEmployeeById(subordinateId))
                .ToList()!;
        }
    }
}