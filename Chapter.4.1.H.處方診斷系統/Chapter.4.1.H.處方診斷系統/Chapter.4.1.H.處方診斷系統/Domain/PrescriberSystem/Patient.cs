using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Chapter._4._1.H.處方診斷系統.Domain.PrescriberSystem;

public class Patient
{
    public Patient(string id, string name, Gender gender, int age, float height, float weight)
    {
        Validate(id, name, age, height, weight);

        Id = id;
        Name = name;
        Gender = gender;
        Age = age;
        Height = height;
        Weight = weight;
    }

    public string Id { get; }

    public string Name { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; }

    public int Age { get; }

    public float Height { get; }

    public float Weight { get; }
    private List<Case> Cases { get; } = new();
    
    public void AddCase(Case newCase) => Cases.Add(newCase);

    private static void Validate(string id, string name, int age, float height, float weight)
    {
        if (Regex.IsMatch(id, @"^[A-Z]\d{9}$") is false)
        {
            throw new ArgumentException("身分證字號格式錯誤");
        }

        if (name.Length is >= 4 and <= 30 is false)
        {
            throw new ArgumentException("姓名長度錯誤");
        }

        if (age is > 0 and <= 180 is false)
        {
            throw new ArgumentException("年齡範圍錯誤");
        }

        if (height is > 0 and <= 500 is false)
        {
            throw new ArgumentException("身高範圍錯誤");
        }

        if (weight is > 0 and <= 500 is false)
        {
            throw new ArgumentException("體重範圍錯誤");
        }
    }
}