namespace Chapter._3._2.H.快捷鍵設置;

public class Macro
{
    private readonly Tank _tank;
    private readonly Telecom _telecom;
    public readonly Dictionary<char, List<Action>> ActionLookup = new();

    public Macro(Tank tank, Telecom telecom)
    {
        _tank = tank;
        _telecom = telecom;
    }

    public char[] Keyboard { get; set; }

    public void BindAction()
    {
        Console.Write("選擇要設定的 Key:");
        var key = char.Parse(Console.ReadLine());
        Console.WriteLine($"要將哪些指令設置成快捷鍵 {key} 的巨集（輸入多個數字，以空白隔開）:");
        ShowAllCommand();
        var input = Console.ReadLine();
        var commands = input.Split(' ').Select(int.Parse);

        foreach (var command in commands)
        {
            var containsKey = ActionLookup.ContainsKey(key);
            if (containsKey is false)
            {
                switch (command)
                {
                    case 0:
                        ActionLookup.Add(key, new List<Action>() {() => _tank.MoveForward()});
                        break;
                    case 1:
                        ActionLookup.Add(key, new List<Action>() {() => _tank.MoveBackward()});
                        break;
                    case 2:
                        ActionLookup.Add(key, new List<Action>() {() => _telecom.Connect()});
                        break;
                    case 3:
                        ActionLookup.Add(key, new List<Action>() {() => _telecom.Disconnect()});
                        break;
                    case 4:
                        ActionLookup.Add(key, new List<Action>() {Reset});
                        break;
                }
            }
            else
            {
                switch (command)
                {
                    case 0:
                        ActionLookup[key].Add(() => _tank.MoveForward());
                        break;
                    case 1:
                        ActionLookup[key].Add(() => _tank.MoveBackward());
                        break;
                    case 2:
                        ActionLookup[key].Add(() => _telecom.Connect());
                        break;
                    case 3:
                        ActionLookup[key].Add(() => _telecom.Disconnect());
                        break;
                    case 4:
                        ActionLookup[key].Add(Reset);
                        break;
                }
            }
        }
    }

    public void Reset()
    {
        ActionLookup.Clear();
    }

    public void Undo()
    {
    }

    public void Redo()
    {
    }

    private void ShowAllCommand()
    {
        Console.WriteLine("(0) MoveTankForward");
        Console.WriteLine("(1) MoveTankBackward");
        Console.WriteLine("(2) ConnectTelecom");
        Console.WriteLine("(3) DisconnectTelecom");
        Console.WriteLine("(4) ResetMainControlKeyboard");
    }
}