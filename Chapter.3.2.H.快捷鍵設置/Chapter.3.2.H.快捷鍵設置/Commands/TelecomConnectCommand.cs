using Chapter._3._2.H.快捷鍵設置.MilitaryDevice;

namespace Chapter._3._2.H.快捷鍵設置.Commands;

public class TelecomConnectCommand : ICommand
{
    private readonly Telecom _telecom;

    public TelecomConnectCommand(Telecom telecom)
    {
        _telecom = telecom;
    }


    public string Name => $"Connect{nameof(Telecom)}";

    public void Execute()
    {
        _telecom.Connect();
    }

    public void Undo()
    {
        _telecom.Disconnect();
    }
}