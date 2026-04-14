using ReversedOfClans.Logic;

namespace ReversedOfClans.Logic
{
    public record Player
    {
        public int HighID { get; init; }
        public int LowID { get; init; }
        public string? Token { get; init; }
        public Messaging Messaging { get; init; }

        public Player(int highID, int lowID, string? token, Messaging Messaging)
        {
            if (highID < 0)
            {
                throw new ArgumentException("HighID cannot be negative", nameof(highID));
            }
            if (lowID < 0)
            {
                throw new ArgumentException("LowID cannot be negative", nameof(lowID));
            }

            HighID = highID;
            LowID = lowID;
            Token = token;
            Messaging = Messaging;
        }

        public Player(Messaging Messaging) : this(0, 0, null, Messaging)
        {
        }
    }
}
