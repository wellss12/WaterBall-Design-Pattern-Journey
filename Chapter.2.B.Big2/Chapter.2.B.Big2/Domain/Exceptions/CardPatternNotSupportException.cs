namespace Chapter._2.B.Big2.Domain.Exceptions;

public class CardPatternNotSupportException : Exception
{
    public CardPatternNotSupportException():base("此牌型不合法，請再嘗試一次。")
    {
        
    }
}