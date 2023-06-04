using Chapter._3._2.H.快捷鍵設置.Commands;

namespace Chapter._3._2.H.快捷鍵設置;

public class Macro
{
    public readonly Dictionary<char, IEnumerable<ICommand>> CommandLookup = new();
    private readonly Queue<KeyValuePair<char, IEnumerable<ICommand>>> _queue = new();
    public readonly Queue<ICommand> UndoQueue = new();
    public readonly Queue<ICommand> RedoQueue = new();

    public void BindAction(char key, IEnumerable<ICommand> commands)
    {
        CommandLookup.Add(key, commands);
    }

    public void ResetKeyboard()
    {
        foreach (var keyValuePair in CommandLookup)
        {
            _queue.Enqueue(keyValuePair);
        }

        CommandLookup.Clear();
    }

    public void RestoreKeyboard()
    {
        while (_queue.Any())
        {
            var keyValuePair = _queue.Dequeue();
            CommandLookup.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }

    public void Undo()
    {
        while (UndoQueue.Any())
        {
            var command = UndoQueue.Dequeue();
            command.Undo();
            RedoQueue.Enqueue(command);
        }
    }

    public void Redo()
    {
        while (RedoQueue.Any())
        {
            var command = RedoQueue.Dequeue();
            command.Execute();
            UndoQueue.Enqueue(command);
        }
    }

    public void ShowAllBindingCommand()
    {
        foreach (var (key, value) in CommandLookup)
        {
            var enumerable = value.Select(command => $"{command.GetType().Name.Replace("Command", "")}");
            var join = string.Join(" & ", enumerable);

            Console.WriteLine($"{key}: {join}");
        }
    }
}