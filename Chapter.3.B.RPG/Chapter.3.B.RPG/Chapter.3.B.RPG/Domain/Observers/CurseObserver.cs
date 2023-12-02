using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Observers;

public class CurseObserver : IRoleDeadObserver
{
    public readonly Role Cursator;
    public CurseObserver(Role cursator)
    {
        Cursator = cursator;
    }
    
    public void Update(Role cursatee)
    {
        if (Cursator.IsAlive())
        {
            Cursator.Hp += cursatee.Mp;
        }
    }
}