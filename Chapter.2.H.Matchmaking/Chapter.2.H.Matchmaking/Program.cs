using System.Collections.Immutable;

internal class Program
{
    private static readonly IReadOnlyList<string> AllHabits = new List<string>()
    {
        "打籃球", "打桌球", "打棒球", "游泳", "睡覺", "看書", "耍費", "看電影", "打程式", "看劇"
    };

    private static readonly List<Individual> Individuals = new();

    static void Main(string[] args)
    {
        InitIndividuals();

        Console.WriteLine("使用 距離先決 的配對方式");
        DistanceBasedMatch();

        Console.WriteLine("----------------------------------------------");

        Console.WriteLine("使用 興趣先決 的配對方式");
        HabitBasedMatch();

        Console.WriteLine("----------------------------------------------");

        Console.WriteLine("使用 距離先決-反向 的配對方式");
        ReverseDistanceBasedMatch();

        Console.WriteLine("----------------------------------------------");

        Console.WriteLine("使用 興趣先決-反向 的配對方式");
        ReverseHabitBasedMatch();

        Console.ReadLine();
    }

    private static void InitIndividuals()
    {
        var random = new Random();

        for (var i = random.Next(5, 20); i > 0; i--)
        {
            var gender = i < 10 ? Gender.Male : Gender.Female;
            var habits = AllHabits
                .OrderBy(t => random.Next())
                .Take(5)
                .ToImmutableList();
            var coord = new Coord(random.Next(0, 20), random.Next(0, 20));
            var individual = new Individual(i, 18 + i, gender, $"Hello{i}", habits, coord);

            Individuals.Add(individual);
        }
    }

    private static void DistanceBasedMatch()
    {
        var system = new MatchmakingSystem(Individuals, new DistanceBasedMatchmakingStrategy());
        system.Match();
        DisplayDistanceBasedResult();
    }

    private static void DisplayDistanceBasedResult()
    {
        foreach (var individual in Individuals)
        {
            var distance = individual.Coord.GetDistance(individual.Matchee.Coord);
            Console.WriteLine($"{individual.Id} 匹配了 {individual.Matchee.Id} \n 距離相差 {distance}");
        }
    }

    private static void HabitBasedMatch()
    {
        var system = new MatchmakingSystem(Individuals, new HabitBasedMatchmakingStrategy());
        system.Match();
        DisplayHabitBasedResult();
    }

    private static void DisplayHabitBasedResult()
    {
        foreach (var individual in Individuals)
        {
            var habits = string.Join(',', individual.Habits.Intersect(individual.Matchee.Habits));
            Console.WriteLine($"{individual.Id} 匹配了 {individual.Matchee.Id} \n 共同興趣為 {habits}");
        }
    }

    private static void ReverseDistanceBasedMatch()
    {
        var system = new MatchmakingSystem(Individuals,
            new ReverseDecorator(new DistanceBasedMatchmakingStrategy()));
        system.Match();
        DisplayDistanceBasedResult();
    }

    private static void ReverseHabitBasedMatch()
    {
        var system = new MatchmakingSystem(Individuals,
            new ReverseDecorator(new HabitBasedMatchmakingStrategy()));
        system.Match();
        DisplayHabitBasedResult();
    }
}