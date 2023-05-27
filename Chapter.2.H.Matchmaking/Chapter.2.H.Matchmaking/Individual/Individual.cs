public class Individual
{
    public Individual(int id, int age, Gender gender, string intro, IReadOnlyList<string> habits, Coord coord)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Must greater than 0");
        }

        if (age < 18)
        {
            throw new ArgumentException("Must greater than or equal to 18");
        }

        if (intro.Length > 200)
        {
            throw new ArgumentException("Word count must less than or equal to 200");
        }

        Id = id;
        Age = age;
        Gender = gender;
        Intro = intro;
        Habits = habits;
        Coord = coord;
    }

    public int Id { get; }

    public Gender Gender { get; }

    public int Age { get; }

    public string Intro { get; }

    public IReadOnlyList<string> Habits { get; }
    public Coord Coord { get; }

    public Individual Matchee { get; private set; }

    public void Match(Individual matchee)
    {
        Matchee = matchee;
    }

    public IEnumerable<string> GetCommonInterests(IEnumerable<string> habits)
    {
        return Habits.Intersect(habits);
    }
}