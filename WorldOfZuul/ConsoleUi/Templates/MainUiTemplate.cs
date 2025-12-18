using WorldOfZuul.Domain;
using WorldOfZuul.Domain.Rooms;
namespace WorldOfZuul.ConsoleUi.Templates;

public class MainUiTemplate
{
    
    RoomUiTemplate  _roomUi = new RoomUiTemplate();


    
    public void RenderMain(int CurrentDay, int MaxDay, int TurnsLeft, int MaxTurnPerDay, DataHandler dh, string CommandText)
    {
        Console.Clear();

        Console.WriteLine($"Day {CurrentDay} of {MaxDay}");
        Console.WriteLine($"Turns left: {TurnsLeft} of {MaxTurnPerDay}");
        Console.WriteLine();

        _roomUi.RenderRoom(dh);

        

        if (CommandText != null)
        {
            Console.WriteLine(CommandText);
            Console.WriteLine();
        }

        
        if(dh.CurrentRoom is Village)
        {
            Console.WriteLine("General Commands:");
            Console.WriteLine(" - ls v                                 : List out all villagers and their status");
            Console.WriteLine(" - ls r                                 : List out all rooms");
            Console.WriteLine(" - ls j                                 : List out all jobs");
            Console.WriteLine(" - cd [ROOM NAME]                       : Goes to room\n");                 
        }
        Console.WriteLine("Type a command to perform the action.\n");


    }
}