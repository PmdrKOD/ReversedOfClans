using ReversedOfClans.Logic;

namespace ReversedOfClans.Packets.Factory
{
    public interface PiranhaMessageFactory
    {
        PiranhaMessage Create(byte[] data, Messaging Messaging);
    }
}
