namespace Chapter._3._2.H.快捷鍵設置;

public class MainController
{
    private readonly Tank _tank;
    private readonly Telecom _telecom;
    private readonly Dictionary<char, Action> _actionLookup = new();
    private readonly Macro _macro;

    public MainController(Tank tank, Telecom telecom)
    {
        _tank = tank;
        _telecom = telecom;
        _macro = new Macro(tank, telecom);
    }


    private void Reset()
    {
        Console.WriteLine($"The MainController has reset keyboard.");
        _actionLookup.Clear();
    }

    public void BindAction()
    {
        Console.Write("設置巨集指令 (y/n)：");
        var answer = char.Parse(Console.ReadLine());
        bool isUseMacro = false;
        if (answer == 'y')
        {
            isUseMacro = true;
        }
        else if (answer == 'n')
        {
            isUseMacro = false;
        }


        if (isUseMacro)
        {
            _macro.BindAction();
        }
        else if (isUseMacro is false)
        {
            Console.Write("選擇要設定的 Key:");
            var key = char.Parse(Console.ReadLine());
            Console.WriteLine($"要將哪一道指令設置到快捷鍵 {key} 上:");
            ShowAllCommand();
            var command = int.Parse(Console.ReadLine());

            switch (command)
            {
                case 0:
                    _actionLookup.Add(key, () => _tank.MoveForward());
                    break;
                case 1:
                    _actionLookup.Add(key, () => _tank.MoveBackward());
                    break;
                case 2:
                    _actionLookup.Add(key, () => _telecom.Connect());
                    break;
                case 3:
                    _actionLookup.Add(key, () => _telecom.Disconnect());
                    break;
                case 4:
                    _actionLookup.Add(key, Reset);
                    break;
            }
        }
    }

    public void Press(char key)
    {
        if (_actionLookup.TryGetValue(key, out var command))
        {
            command();
        }
        else if (_macro.ActionLookup.TryGetValue(key, out var commands))
        {
            commands.ForEach(t => t.Invoke());
        }
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