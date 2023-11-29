using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain.Observers;

public interface IRoleDeadObserver
{
    public void Update(Role role);
}