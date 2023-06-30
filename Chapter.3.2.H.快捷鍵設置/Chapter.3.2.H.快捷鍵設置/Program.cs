using Chapter._3._2.H.快捷鍵設置;
using Chapter._3._2.H.快捷鍵設置.Commands;
using Chapter._3._2.H.快捷鍵設置.MilitaryDevice;

public class Program
{
    private static readonly MainController MainController = new();

    private static readonly Dictionary<char, ICommand> DefaultCommandLookup = new()
    {
        {'0', new TankMoveForwardCommand(new Tank())},
        {'1', new TankMoveBackwardCommand(new Tank())},
        {'2', new TelecomConnectCommand(new Telecom())},
        {'3', new TelecomDisconnectCommand(new Telecom())},
        {'4', new MainControllerResetCommand(MainController)}
    };

    public static void Main()
    {
        while (true)
        {
            Console.Write("(1) 快捷鍵設置 (2) Undo (3) Redo (字母) 按下按鍵: ");
            var input = Console.ReadLine();
            if (input == "1")
            {
                Console.Write("設置巨集指令 (y/n)：");
                var answer = char.Parse(Console.ReadLine());
                var isUseMacro = answer switch
                {
                    'y' => true,
                    'n' => false,
                    _ => false
                };

                HandleBindingSetting(isUseMacro);
            }
            else if (input == "2")
            {
                MainController.Undo();
            }
            else if (input == "3")
            {
                MainController.Redo();
            }
            else
            {
                MainController.Press(char.Parse(input));
            }

            MainController.ShowAllShortcutKey();
        }
    }

    private static void HandleBindingSetting(bool isUseMacro)
    {
        var key = GetBindingKey();
        ShowBindingKeyMessage(isUseMacro, key);
        ShowAllCommand();
        var commands = GetBindingCommands();
        BindCommands(key, commands);
    }

    private static void BindCommands(char key, IEnumerable<ICommand> commands)
    {
        MainController.BindCommands(key, commands);
    }

    private static IEnumerable<ICommand> GetBindingCommands()
    {
        var commandsInput = Console.ReadLine();
        var commandsBySplit = commandsInput.Split(' ').Select(char.Parse);

        var allCommands = new List<ICommand>();
        foreach (var commandIndex in commandsBySplit)
        {
            if (DefaultCommandLookup.TryGetValue(commandIndex, out var defaultCommands))
            {
                allCommands.Add(defaultCommands);
            }
            else if (MainController.ShortKeyCommandLookup.TryGetValue(commandIndex, out var shortKeyCommands))
            {
                allCommands.AddRange(shortKeyCommands);
            }
            else
            {
                throw new ArgumentException($"Command: {commandIndex} unsupported");
            }
        }

        return allCommands;
    }

    private static char GetBindingKey()
    {
        Console.Write("選擇要設定的 Key: ");
        return char.Parse(Console.ReadLine());
    }

    private static void ShowBindingKeyMessage(bool isUseMacro, char key)
    {
        var bindingKeyMessage = isUseMacro
            ? $"要將哪些指令設置成快捷鍵 {key} 的巨集（輸入多個數字，以空白隔開）:"
            : $"要將哪一道指令設置到快捷鍵 {key} 上:";
        Console.WriteLine(bindingKeyMessage);
    }

    private static void ShowAllCommand()
    {
        foreach (var (key, command) in DefaultCommandLookup)
        {
            Console.WriteLine($"({key}) {command.Name}");
        }

        foreach (var (key, commands) in MainController.ShortKeyCommandLookup)
        {
            var shortKeyCommandNameByJoin = string.Join(" & ", commands.Select(command => command.Name));
            Console.WriteLine($"({key}) {shortKeyCommandNameByJoin}");
        }
    }
}