using Chapter._3._2.H.快捷鍵設置.Commands;

namespace Chapter._3._2.H.快捷鍵設置;

public class MainController
{
    private readonly Dictionary<char, ICommand> _commandLookup = new();
    private readonly Queue<KeyValuePair<char, ICommand>> _queue = new();
    private readonly Stack<ICommand> _undoStack = new();
    private readonly Stack<ICommand> _redoStack = new();
    public readonly Macro Macro;

    public MainController()
    {
        Macro = new Macro();
    }

    public void ShowAllBindingCommand()
    {
        Macro.ShowAllBindingCommand();
        foreach (var (key, value) in _commandLookup)
        {
            Console.WriteLine($"{key}: {value.GetType().Name.Replace("Command", "")}");
        }
    }

    public void ResetKeyboard()
    {
        Console.WriteLine($"The {nameof(MainController)} has reset keyboard.");
        foreach (var keyValuePair in _commandLookup)
        {
            _queue.Enqueue(keyValuePair);
        }

        _commandLookup.Clear();
        Macro.ResetKeyboard();
    }

    public void RestoreKeyboard()
    {
        Console.WriteLine($"The {nameof(MainController)} has restore keyboard.");
        while (_queue.Count > 0)
        {
            var keyValuePair = _queue.Dequeue();
            _commandLookup.Add(keyValuePair.Key, keyValuePair.Value);
        }

        Macro.RestoreKeyboard();
    }

    public void BindAction(char key, ICommand command)
    {
        _commandLookup.Add(key, command);
    }

    public void Press(char key)
    {
        if (_commandLookup.TryGetValue(key, out var command))
        {
            _undoStack.Push(command);
            command.Execute();
            _redoStack.Clear();
        }
        else if (Macro.CommandLookup.TryGetValue(key, out var commands))
        {
            foreach (var command1 in commands)
            {
                Macro.UndoQueue.Enqueue(command1);
                command1.Execute();
                Macro.RedoQueue.Clear();
            }
        }
    }

    public void Undo()
    {
        if (_undoStack.Any())
        {
            var command = _undoStack.Pop();
            command.Undo();
            _redoStack.Push(command);
        }
        
        Macro.Undo();
    }

    public void Redo()
    {
        if (_redoStack.Any())
        {
            var command = _redoStack.Pop();
            command.Execute();
            _undoStack.Push(command);
        }
        
        Macro.Redo();
    }
}