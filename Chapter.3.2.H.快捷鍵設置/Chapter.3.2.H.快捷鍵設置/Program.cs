using Chapter._3._2.H.快捷鍵設置;
using Chapter._3._2.H.快捷鍵設置.Commands;
using Chapter._3._2.H.快捷鍵設置.Exceptions;
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

    private static bool _isUseMacro;

    public static void Main()
    {
        while (true)
        {
            Console.Write("(1) 快捷鍵設置 (2) Undo (3) Redo (字母) 按下按鍵: ");
            var input = Console.ReadLine();
            try
            {
                HandleInput(input);
            }
            catch (KeyBoardUnsupportedException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (CommandUnsupportedException exception)
            {
                Console.WriteLine(exception.Message);
            }

            MainController.ShowAllShortcutKey();
        }
    }

    private static void HandleInput(string? input)
    {
        switch (input)
        {
            case "1":
                DecideMacroUsage();
                HandleBindingSetting();
                break;
            case "2":
                MainController.Undo();
                break;
            case "3":
                MainController.Redo();
                break;
            default:
                MainController.Press(char.Parse(input));
                break;
        }
    }

    private static void DecideMacroUsage()
    {
        Console.Write("設置巨集指令 (y/n)：");
        var keyInfo = Console.ReadKey();
        Console.WriteLine();
        var yesOrNo = keyInfo.KeyChar;
    
        _isUseMacro = yesOrNo switch
        {
            'y' => true,
            'n' => false,
            _ => false
        };
    }

    private static void HandleBindingSetting()
    {
        var key = GetBindingKey();
        MainController.Keyboard.ValidateKey(key);
        ShowBindingKeyMessage(key);
        ShowAllCommand();
        var commands = GetBindingCommands();
        MainController.BindCommands(key, commands);
    }

    private static IEnumerable<ICommand> GetBindingCommands()
    {
        var commandInput = Console.ReadLine();
        return _isUseMacro
            ? GetMacroCommands(commandInput)
            : GetCommands(commandInput);
    }

    private static IEnumerable<ICommand> GetMacroCommands(string commandInput)
    {
        var commandsBySplit = commandInput.Split(' ');

        var commands = new List<ICommand>();
        foreach (var command in commandsBySplit)
        {
            commands.AddRange(GetCommands(command));
        }

        return commands;
    }

    private static IEnumerable<ICommand> GetCommands(string commandInput)
    {
        var commandKey = char.Parse(commandInput);
        if (DefaultCommandLookup.TryGetValue(commandKey, out var defaultCommand))
        {
            return new List<ICommand> {defaultCommand};
        }

        if (MainController.ShortKeyCommandLookup.TryGetValue(commandKey, out var shortKeyCommands))
        {
            return shortKeyCommands;
        }

        throw new CommandUnsupportedException(commandKey);
    }

    private static char GetBindingKey()
    {
        Console.Write("選擇要設定的 Key: ");
        var keyInfo = Console.ReadKey();
        Console.WriteLine();
        
        return keyInfo.KeyChar;
    }

    private static void ShowBindingKeyMessage(char key)
    {
        var bindingKeyMessage = _isUseMacro
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