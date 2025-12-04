namespace WorldOfZuul.Domain.Rooms;

public class Land : Room
{
    public Land(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {
    }

    public override string GetEnterRoomMessage()
    {
        return $"You have entered {ShortDescription}.\n{LongDescription}";
    }
}