using ReversedOfClans.Utils;

public class LogicLong(int a1,int a2)
{
    public void Encode(ChecksumEncoder encoder)
    {
        encoder.WriteInt(a1);
        encoder.WriteInt(a2);
    }
}