using WorldOfZuul.Domain;
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


    public void RenderRoom(DataHandler dh)
    {
        Console.WriteLine($"You are in: {dh.CurrentRoom?.ShortDescription}");
        Console.WriteLine();
        
        
        Console.WriteLine(dh.CurrentRoom?.GetEnterRoomMessage());

        // Console.WriteLine($"{dh.CurrentRoom?.LongDescription}");

        //dh.CurrentRoom?.GetEnterRoomMessage();


    }
}