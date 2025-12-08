using WorldOfZuul.Domain.Jobs;
using WorldOfZuul.Domain.CommandHandler;

namespace WorldOfZuul.Domain.Rooms;

public interface IRoom
{
    string ShortDescription { get; }
    string LongDescription { get; }
    Dictionary<string, Room> Exits { get; }
    List<Job?> Jobs { get; }
    
    string GetEnterRoomMessage();
    void SetExit(string direction, Room neighbor);
    Room? GetExit(string direction);
    string RoomCommandHandler(Command command);
}