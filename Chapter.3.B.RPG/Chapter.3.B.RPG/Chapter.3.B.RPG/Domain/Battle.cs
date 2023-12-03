using Chapter._3.B.RPG.Domain.Roles;

namespace Chapter._3.B.RPG.Domain;

public class Battle
{
    public readonly Troop T1;
    public readonly Troop T2;

    public Battle(Troop t1, Troop t2)
    {
        if (t1.Roles.First().Name is not "英雄")
        {
            throw new Exception("第一個軍隊必須為英雄");
        }

        T1 = t1;
        T2 = t2;
        T1.Battle = this;
        T2.Battle = this;
    }

    public void Start()
    {
        while (!IsGameOver())
        {
            Round();
        }

        DisplayWinner();
    }

    private void Round()
    {
        var roles = T1.Roles.Concat(T2.Roles);
        for (var i = 0; i < roles.Count(); i++)
        {
            var role = roles.ElementAt(i);
            if (role.IsAlive())
            {
                role.StartAction();
            }

            if (IsGameOver())
            {
                return;
            }
        }
    }

    private void DisplayWinner() => Console.WriteLine(IsHeroDead() ? "你失敗了！" : "你獲勝了！");

    private bool IsGameOver() => IsHeroDead() || HasTroopAnnihilated();

    private bool HasTroopAnnihilated() => T1.IsAnnihilated() || T2.IsAnnihilated();

    private bool IsHeroDead() => FindHero().IsDead();

    private Role FindHero() => T1.Roles.Single(role => role.Name == "英雄");
}