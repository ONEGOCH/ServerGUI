namespace ServerLogic
{
    public class Token
    {
        public readonly int Length;
        public readonly int StartIndex;
        public readonly string Value;

        public Token(string value, int startIndex, int length)
        {
            StartIndex = startIndex;
            Length = length;
            Value = value;
        }

        public int GetIndexNextToToken() => StartIndex + Length;

        public override string ToString() => string.Format("{0} ({1}, {2})", Value, StartIndex, Length);
    }
}