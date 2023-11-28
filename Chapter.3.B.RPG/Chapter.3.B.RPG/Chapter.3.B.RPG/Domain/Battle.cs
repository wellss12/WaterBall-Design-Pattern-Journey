namespace Chapter._3.B.RPG.Domain;

public class Battle
{
    private readonly Troop _t1;
    private readonly Troop _t2;

    public Battle(Troop t1, Troop t2)
    {
        if (t1.Roles.First() is not Hero)
        {
            throw new Exception("第一個軍隊必須為英雄");
        }

        _t1 = t1;
        _t2 = t2;
        _t1.Battle = this;
        _t2.Battle = this;
    }

    public void Start()
    {
        while (!IsGameOver())
        {
            Round();
        }
    }

    private void Round()
    {
        foreach (var role in _t1.Roles.Union(_t2.Roles))
        {
            if (role.IsAlive())
            {
                role.StartAction();
            }

            if (IsGameOver())
            {
                var gameOverMessage = IsHeroDead() ? "你失敗了！" : "你獲勝了！";
                Console.WriteLine(gameOverMessage);
                break;
            }
        }
    }

    private bool IsGameOver() => IsHeroDead() || HasTroopAnnihilated();

    private bool HasTroopAnnihilated() => _t1.IsAnnihilated() || _t2.IsAnnihilated();

    private bool IsHeroDead() => FindHero().IsDead();

    private Role FindHero() => _t1.Roles.Single(role => role is Hero);

    public IEnumerable<Role> GetEnemies(Role target)
    {
        var enemies = target.Troop == _t1
            ? _t2.Roles
            : _t1.Roles;
        
        return enemies.Where(enemy => enemy.IsAlive());
    }
}