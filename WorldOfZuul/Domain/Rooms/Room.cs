using WorldOfZuul.Domain.CommandHandler;
using WorldOfZuul.Domain.Jobs;

namespace WorldOfZuul.Domain.Rooms;

public class Room : IRoom
{
    public string ShortDescription { get; private set; }
    public string LongDescription { get; private set; }
    public Dictionary<string, Room> Exits { get; private set; } = new();
    public List<Job?> Jobs { get; private set; } = new List<Job?>();

    protected Room(string shortDesc, string longDesc)
    {
        ShortDescription = shortDesc;
        LongDescription = longDesc;
    }

    protected Room(string shortDesc, string longDesc, Job job)
    {
        ShortDescription = shortDesc;
        LongDescription = longDesc;
        this.Jobs.Add(job);
    }
    
    public Room(string shortDesc, string longDesc, List<Job?> jobs)
    {
        ShortDescription = shortDesc;
        LongDescription = longDesc;
        this.Jobs = jobs;
    }

    public virtual string GetEnterRoomMessage()
    {
        return $"Entering {ShortDescription}";
    }

    public virtual string GetCommandListMessage(Command command)
    {
        return "No specific commands available in this room.";
    }

    public virtual string RoomCommandHandler(Command command, Resources resources)
    {
        return $"Command '{command.Name}' not recognized in this room.";
    }

    public void SetExit(string direction, Room neighbor)
    {
        Exits[direction] = neighbor;
    }

    public Room? GetExit(string direction)
    {
        return Exits.GetValueOrDefault(direction);
    }
}