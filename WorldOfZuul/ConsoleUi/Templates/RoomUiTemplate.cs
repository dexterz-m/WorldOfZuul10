using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.ConsoleUi.Templates;

public class RoomUiTemplate
{
    // handles room specific commands
    
    // How it should look:
    /*
     * Room name
     * Room description
     *
     * Room specific commands
     */


    public void RenderRoom(Room? currentRoom)
    {
        Console.WriteLine($"You are in: {currentRoom?.ShortDescription}");
        Console.WriteLine();
        
        Console.WriteLine($"{currentRoom?.LongDescription}");
        
        //Temporary

        Console.WriteLine("Room Commands: ");
        Console.WriteLine("ls v                                   List out all villagers and their status");
        Console.WriteLine("ls r                                   List out all rooms");
        Console.WriteLine("ls j                                   List out all jobs");
        Console.WriteLine("cd [ROOM NAME]                         Goes to room");
        
    }
}