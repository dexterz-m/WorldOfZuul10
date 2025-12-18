using WorldOfZuul.Domain;
using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.ConsoleUi.Templates;

public class RoomUiTemplate
{

    

    int fLoad = 0;
    public void RenderRoom(DataHandler dh)
    {
        Console.WriteLine($"You are in: {dh.CurrentRoom?.ShortDescription}");
        Console.WriteLine();
        
        if(dh.t >= 1 || fLoad == 0)
        {
            Console.WriteLine(dh.CurrentRoom?.GetEnterRoomMessage());
            dh.t = 0;
        }

        if(fLoad == 0)
        {
            fLoad = 1;
        }
       

        // Console.WriteLine($"{dh.CurrentRoom?.LongDescription}");

        //dh.CurrentRoom?.GetEnterRoomMessage();


    }
}