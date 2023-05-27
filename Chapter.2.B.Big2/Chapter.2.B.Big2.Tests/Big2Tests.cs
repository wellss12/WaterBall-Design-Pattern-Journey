using FluentAssertions;

namespace Chapter._2.Big2.Tests;

[TestFixture]
public class Big2Tests
{
    private string _filePath;
    private StringWriter _stringWriter;

    [SetUp]
    public void SetUp()
    {
        _stringWriter = new StringWriter();
    }

    [Test]
    public void always_play_first_card()
    {
        GivenFilePath("../../../TestCase/always-play-first-card.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        GetActual().Should().Be(GetExpected());
    }

    [Test]
    public void normal_no_error_play1()
    {
        GivenFilePath("../../../TestCase/normal-no-error-play1.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        GetActual().Should().Be(GetExpected());
    }

    [Test]
    public void normal_no_error_play2()
    {
        GivenFilePath("../../../TestCase/normal-no-error-play2.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        GetActual().Should().Be(GetExpected());
    }

    [Test]
    public void illegal_actions()
    {
        GivenFilePath("../../../TestCase/illegal-actions.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        GetActual().Should().Be(GetExpected());
    }

    [Test]
    public void straight()
    {
        GivenFilePath("../../../TestCase/straight.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        GetActual().Should().Be(GetExpected());
    }

    [Test]
    public void fullHouse()
    {
        GivenFilePath("../../../TestCase/fullhouse.in");
        GivenStringReader();
        GivenStringWriter();

        Program.Main();

        GetActual().Should().Be(GetExpected());
    }

    private string GetActual()
    {
        var actual = _stringWriter.GetStringBuilder().ToString();
        return actual;
    }

    private string GetExpected()
    {
        var expected = File.ReadAllText(_filePath.Replace(".in", ".out"));
        return expected;
    }

    private void GivenStringWriter()
    {
        // 把 Console.Out 導到 stringWriter
        Console.SetOut(_stringWriter);
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
}