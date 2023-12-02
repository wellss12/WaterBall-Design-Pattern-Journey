using FluentAssertions;

namespace Chapter._3.B.RPG.Tests;

public class Tests
{
    private string _filePath;
    private StringWriter _stringWriter;

    [SetUp]
    public void SetUp()
    {
        _stringWriter = new StringWriter();
    }

    [Test]
    public void only_basic_attack()
    {
        GivenFilePath("TestCases/only-basic-attack.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        var actual = GetActual();
        actual.Should().Be(GetExpected());
    }

    [Test]
    public void waterball_and_fireball_1v2()
    {
        GivenFilePath("TestCases/waterball-and-fireball-1v2.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        var actual = GetActual();
        actual.Should().Be(GetExpected());
    }

    [Test]
    public void self_healing()
    {
        GivenFilePath("TestCases/self-healing.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        var actual = GetActual();
        actual.Should().Be(GetExpected());
    }

    [Test]
    public void petrochemical()
    {
        GivenFilePath("TestCases/petrochemical.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        var actual = GetActual();
        actual.Should().Be(GetExpected());
    }

    [Test]
    public void poison()
    {
        GivenFilePath("TestCases/poison.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        var actual = GetActual();
        actual.Should().Be(GetExpected());
    }

    [Test]
    public void summon()
    {
        GivenFilePath("TestCases/summon.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        var actual = GetActual();
        actual.Should().Be(GetExpected());
    }

    [Test]
    public void self_explosion()
    {
        GivenFilePath("TestCases/self-explosion.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        var actual = GetActual();
        actual.Should().Be(GetExpected());
    }

    [Test]
    public void cheerup()
    {
        GivenFilePath("TestCases/cheerup.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        var actual = GetActual();
        actual.Should().Be(GetExpected());
    }

    [Test]
    public void curse()
    {
        GivenFilePath("TestCases/curse.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        var actual = GetActual();
        actual.Should().Be(GetExpected());
    }

    private void GivenFilePath(string path)
    {
        _filePath = path;
    }

    private void GivenStringReader()
    {
        var input = File.ReadAllText(_filePath);
        var stringReader = new StringReader(input);
        // 把檔案內容導入 Console.In
        Console.SetIn(stringReader);
    }

    private void GivenStringWriter()
    {
        // 把 Console.Out 導到 stringWriter
        Console.SetOut(_stringWriter);
    }

    private string GetActual()
    {
        return _stringWriter.GetStringBuilder().ToString();
    }

    private string GetExpected()
    {
        return File.ReadAllText(_filePath.Replace(".in", ".out"));
    }
}