using Chapter._3._2.H.快捷鍵設置.MilitaryDevice;

namespace Chapter._3._2.H.快捷鍵設置.Commands;

public class TelecomDisconnectCommand : ICommand
{
    private readonly Telecom _telecom;

    public TelecomDisconnectCommand(Telecom telecom)
    {
        _telecom = telecom;
    }


    public string Name => $"Disconnect{nameof(Telecom)}";

    public void Execute()
    {
        _telecom.Disconnect();
    }

    public void Undo()
    {
        _telecom.Connect();
    }
}