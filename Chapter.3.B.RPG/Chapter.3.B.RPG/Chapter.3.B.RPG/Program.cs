// See https://aka.ms/new-console-template for more information

using Chapter._3.B.RPG.Domain;
using Chapter._3.B.RPG.Domain.DecisionStrategies;
using Action = Chapter._3.B.RPG.Domain.Actions.Action;

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

            var role = name == "英雄"
                ? new Hero(name, hp, mp, str, new List<Action>(), new CLIDecisionStrategy())
                : new Role(name, hp, mp, str, new List<Action>(), new AIDecisionStrategy());
            roles.Add(role);
        }

        var troop = new Troop(int.Parse(troopStart.Substring(4, 1)), roles);
        roles.ForEach(role => role.Troop = troop);
        return troop;
    }
}