using Chapter._3._2.H.快捷鍵設置.Commands;

namespace Chapter._3._2.H.快捷鍵設置;

public class MainController
{
    private readonly Keyboard _keyboard = new();
    private readonly Dictionary<char, IEnumerable<ICommand>> _commandLookup = new();
    private readonly Queue<KeyValuePair<char, IEnumerable<ICommand>>> _keyboardQueue = new();
    private readonly Stack<IEnumerable<ICommand>> _undoStack = new();
    private readonly Stack<IEnumerable<ICommand>> _redoStack = new();

    public void BindCommands(char key, IEnumerable<ICommand> commands)
    {
        _keyboard.ValidateKey(key);
        _commandLookup.Add(key, commands);
    }

    public void Press(char key)
    {
        _keyboard.ValidateKey(key);
        if (_commandLookup.TryGetValue(key, out var commands))
        {
            var queue = new Queue<ICommand>();
            foreach (var command in commands)
            {
                queue.Enqueue(command);
                command.Execute();
            }

            _undoStack.Push(queue);
            _redoStack.Clear();
        }
        else
        {
            Console.WriteLine($"Keyboard: {key.ToString()} unsupported");
        }
    }

    public void Undo()
    {
        if (_undoStack.Any())
        {
            var commands = _undoStack.Pop();
            var queue = new Queue<ICommand>();
            foreach (var command in commands)
            {
                command.Undo();
                queue.Enqueue(command);
            }

            _redoStack.Push(queue);
        }
    }

    public void Redo()
    {
        if (_redoStack.Any())
        {
            var commands = _redoStack.Pop();
            var queue = new Queue<ICommand>();
            foreach (var command in commands)
            {
                command.Execute();
                queue.Enqueue(command);
            }

            _undoStack.Push(queue);
        }
    }

    public void ResetKeyboard()
    {
        Console.WriteLine($"The {nameof(MainController)} has reset keyboard.");

        foreach (var keyValuePair in _commandLookup)
        {
            _keyboardQueue.Enqueue(keyValuePair);
        }

        _commandLookup.Clear();
    }

    public void RestoreKeyboard()
    {
        Console.WriteLine($"The {nameof(MainController)} has restore keyboard.");

        while (_keyboardQueue.Any())
        {
            var keyValuePair = _keyboardQueue.Dequeue();
            _commandLookup.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }

    public void ShowAllBindingCommand()
    {
        foreach (var (key, value) in _commandLookup)
        {
            var allCommandName = value.Select(command => command.Name);
            var allCommandNameByJoin = string.Join(" & ", allCommandName);

            Console.WriteLine($"{key}: {allCommandNameByJoin}");
        }
    }
}