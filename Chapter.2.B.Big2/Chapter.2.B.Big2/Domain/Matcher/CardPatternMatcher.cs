using Chapter._2.B.Big2.Domain.CardPatterns;
using Chapter._2.B.Big2.Domain.Deck;
using Chapter._2.B.Big2.Domain.Exceptions;

namespace Chapter._2.B.Big2.Domain.Matcher;

public abstract class CardPatternMatcher
{
    private readonly CardPatternMatcher? _nextMatcher;

    protected CardPatternMatcher(CardPatternMatcher? nextMatcher)
    {
        _nextMatcher = nextMatcher;
    }

    public CardPattern DecideCardPattern(IEnumerable<Card> cards)
    {
        if (IsMatch(cards))
        {
            return GetCardPattern(cards);
        }
        
        return _nextMatcher is not null
            ? _nextMatcher.DecideCardPattern(cards)
            : throw new CardPatternNotSupportException();
    }
    protected abstract bool IsMatch(IEnumerable<Card> cards);
    protected abstract CardPattern GetCardPattern(IEnumerable<Card> cards);
}
