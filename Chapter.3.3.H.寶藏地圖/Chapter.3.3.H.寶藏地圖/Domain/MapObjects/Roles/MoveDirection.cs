using System.ComponentModel;
using System.Reflection;

namespace Chapter._3._3.H.寶藏地圖.Domain.MapObjects.Roles;

public enum Direction
{
    [Description("↑")] Up = 1,
    [Description("→")] Right = 2,
    [Description("↓")] Down = 3,
    [Description("←")] Left = 4
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        return type
            .GetField(type.GetEnumName(value))
            .GetCustomAttribute<DescriptionAttribute>().Description;
    }
}