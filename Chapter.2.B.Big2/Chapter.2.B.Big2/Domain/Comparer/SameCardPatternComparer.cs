using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Deck;

namespace Chapter._2.B.Big2.Domain.Comparer;

public abstract class SameCardPatternComparer
{
    protected abstract Card GetMaxCard(CardPattern cardPattern);

    /// <summary>
    /// 發現他們都是取出牌型中最大的牌，在以 Card 來 Compare
    /// 所以將 Interface 改成 Abstract，再套用 Template Method
    /// 減少重複程式碼
    /// </summary>
    /// <param name="card1"></param>
    /// <param name="card2"></param>
    /// <returns></returns>
    public int Compare(CardPattern card1, CardPattern card2)
    {
        var maxCard1 = GetMaxCard(card1);
        var maxCard2 = GetMaxCard(card2);
        return maxCard1.Compare(maxCard2);
    }
}