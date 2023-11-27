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
    }

    public bool IsAnnihilated() => Roles.All(role => role.IsDead());

    public void Join(Role role)
    {
        Roles.Add(role);
    }

    public bool HasRoles() => Roles.Any();
}