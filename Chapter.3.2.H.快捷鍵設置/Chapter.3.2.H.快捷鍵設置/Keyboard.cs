using System.Collections;

namespace Chapter._3._2.H.快捷鍵設置;

public class Keyboard : IEnumerable<char>
{
    private IEnumerable<char> Keys { get; }

    public Keyboard()
    {
        Keys = Enumerable.Range('a', 26).Select(x => (char) x);
    }

    public IEnumerator<char> GetEnumerator()
    {
        return Keys.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void ValidateKey(char key)
    {
        if (Keys.Any(t => t == key) is false)
        {
            throw new Exception($"Keyboard: {key.ToString()} unsupported");
        }
    }
}