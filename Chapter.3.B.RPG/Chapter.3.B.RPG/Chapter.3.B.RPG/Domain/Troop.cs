using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain;

public class Troop
{
    public int Number { get; }
    public Battle Battle { get; set; }

    public readonly List<Role> Roles;

    public Troop(int number, List<Role> roles)
    {
        Number = number;
        Roles = roles;
        roles.ForEach(role => role.Troop = this);
    }

    public bool IsAnnihilated() => Roles.All(role => role.IsDead());

    public void Join(Role role)
    {
        Roles.Add(role);
        role.Troop = this;
    }
}