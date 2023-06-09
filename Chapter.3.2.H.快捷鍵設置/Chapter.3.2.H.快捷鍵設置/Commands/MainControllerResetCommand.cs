using Chapter._3._2.H.快捷鍵設置.MasterControlRoom;

namespace Chapter._3._2.H.快捷鍵設置.Commands;

public class MainControllerResetCommand : ICommand
{
    private readonly MainController _mainController;

    public MainControllerResetCommand(MainController mainController)
    {
        _mainController = mainController;
    }

    public string Name => $"Reset{nameof(MainController)}Keyboard";

    public void Execute()
    {
        _mainController.ResetKeyboard();
    }

    public void Undo()
    {
        _mainController.RestoreKeyboard();
    }
}