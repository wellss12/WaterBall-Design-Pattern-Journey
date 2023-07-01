namespace Chapter._3._2.H.快捷鍵設置.Exceptions;

public class CommandUnsupportedException : Exception
{
    public CommandUnsupportedException(char commandKey):base($"Command: {commandKey} unsupported")
    {
        
    }
}