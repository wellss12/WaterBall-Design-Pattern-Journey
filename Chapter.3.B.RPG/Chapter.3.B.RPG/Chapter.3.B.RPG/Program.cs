using Chapter._3.B.RPG.Domain;
using Chapter._3.B.RPG.Domain.Actions.Skills;
using Chapter._3.B.RPG.Domain.Actions.Skills.OnePunchHandlers;
using Chapter._3.B.RPG.Domain.DecisionStrategies;
using Chapter._3.B.RPG.Domain.Roles;
using Action = Chapter._3.B.RPG.Domain.Actions.Action;

namespace Chapter._3.B.RPG;

public class Program
{
    public static void Main()
    {
        // Console.WriteLine 預設會產生 \r\n 
        // 但 測資都是 \n，所以加上這個全域設定
        // 讓 Console.WriteLine 預設產生 \n
        Console.Out.NewLine = "\n";

        var t1 = GetTroop();
        var t2 = GetTroop();
        var battle = new Battle(t1, t2);
        battle.Start();
    }

    private static Troop GetTroop()
    {
        var roles = new List<Role>();
        var troopStart = Console.ReadLine();
        while (true)
        {
            var input = Console.ReadLine();
            if (input.EndsWith("結束"))
            {
                break;
            }

            var roleInfo = input.Split(' ');
            var name = roleInfo[0];
            var hp = int.Parse(roleInfo[1]);
            var mp = int.Parse(roleInfo[2]);
            var str = int.Parse(roleInfo[3]);
            var skills = GetSkills(roleInfo[4..]).ToList();

            DecisionStrategy strategy = name == "英雄"
                ? new CLIDecisionStrategy()
                : new AIDecisionStrategy();
            var role = new Role(name, hp, mp, str, skills, strategy);
            roles.Add(role);
        }

        var number = int.Parse(troopStart.Substring(4, 1));
        return new Troop(number, roles);
    }

    private static IEnumerable<Action> GetSkills(string[] skillNames)
    {
        var actionMap = new Dictionary<string, Func<Action>>()
        {
            {"水球", () => new Waterball()},
            {"火球", () => new Fireball()},
            {"自我治療", () => new SelfHealing()},
            {"石化", () => new Petrochemical()},
            {"下毒", () => new Poison()},
            {"召喚", () => new Summon()},
            {"自爆", () => new SelfExplosion()},
            {"鼓舞", () => new Cheerup()},
            {"詛咒", () => new Curse()},
            {"一拳攻擊", () => new OnePunch(
                new HpGreaterThanOrEqualTo500Handler(
                    new IsPoisonedOrPetrochemicalHandler(
                        new IsCheerupHandler(
                            new IsNormalHandler(null)))))
            }
        };

        return skillNames
            .Where(skill => actionMap.ContainsKey(skill))
            .Select(skill => actionMap[skill]());
    }
}