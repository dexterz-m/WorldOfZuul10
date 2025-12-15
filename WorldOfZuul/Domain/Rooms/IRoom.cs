using WorldOfZuul.Domain.Jobs;
using WorldOfZuul.Domain.CommandHandler;

namespace WorldOfZuul.Domain.Rooms;

public interface IRoom
{
    string ShortDescription { get; }
    string LongDescription { get; }
    //Dictionary<string, Room> Exits { get; }
    List<Job?> Jobs { get; }
    
    string GetEnterRoomMessage();
    string RoomCommandHandler(Command command, Resources resources);
}