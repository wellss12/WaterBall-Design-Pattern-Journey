namespace Chapter._3._2.H.快捷鍵設置.Exceptions;

public class KeyBoardUnsupportedException : Exception
{
    public KeyBoardUnsupportedException(char key) : base($"Keyboard: {key} unsupported")
    {
    }   
}