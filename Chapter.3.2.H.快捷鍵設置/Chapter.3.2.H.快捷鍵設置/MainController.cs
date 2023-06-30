using Chapter._3._2.H.快捷鍵設置.Commands;

namespace Chapter._3._2.H.快捷鍵設置;

public class MainController
{
    public readonly Dictionary<char, IEnumerable<ICommand>> ShortKeyCommandLookup = new();
    public readonly Keyboard Keyboard = new();
    private readonly Queue<KeyValuePair<char, IEnumerable<ICommand>>> _keyboardQueue = new();
    private readonly Stack<IEnumerable<ICommand>> _undoStack = new();
    private readonly Stack<IEnumerable<ICommand>> _redoStack = new();

    public void BindCommands(char key, IEnumerable<ICommand> commands)
    {
        Keyboard.ValidateKey(key);
        ShortKeyCommandLookup.Add(key, commands);
    }

    public void Press(char key)
    {
        Keyboard.ValidateKey(key);
        if (ShortKeyCommandLookup.TryGetValue(key, out var commands))
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

        foreach (var keyValuePair in ShortKeyCommandLookup)
        {
            _keyboardQueue.Enqueue(keyValuePair);
        }

        ShortKeyCommandLookup.Clear();
    }

    public void RestoreKeyboard()
    {
        Console.WriteLine($"The {nameof(MainController)} has restore keyboard.");

        while (_keyboardQueue.Any())
        {
            var keyValuePair = _keyboardQueue.Dequeue();
            ShortKeyCommandLookup.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }

    public void ShowAllShortcutKey()
    {
        foreach (var (key, value) in ShortKeyCommandLookup)
        {
            var allCommandName = value.Select(command => command.Name);
            var allCommandNameByJoin = string.Join(" & ", allCommandName);

            Console.WriteLine($"{key}: {allCommandNameByJoin}");
        }
    }
}