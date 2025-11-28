using WorldOfZuul.Domain.Rooms;
namespace WorldOfZuul.ConsoleUi.Templates;

public class MainUiTemplate
{
    
    RoomUiTemplate  _roomUi = new RoomUiTemplate();


    
    public void RenderMain(int CurrentDay, int MaxDay, int TurnsLeft, int MaxTurnPerDay, Room? _CurrentRoom)
    {
        Console.Clear();

        Console.WriteLine($"Day {CurrentDay} of {MaxDay}");
        Console.WriteLine($"Turns left: {TurnsLeft} of {MaxTurnPerDay}");
        Console.WriteLine();

        _roomUi.RenderRoom(_CurrentRoom);
        Console.WriteLine();

        Console.WriteLine("General Commands:");
        Console.WriteLine("ls v                                   List out all villagers and their status");
        Console.WriteLine("ls r                                   List out all rooms");
        Console.WriteLine("ls j                                   List out all jobs");
        Console.WriteLine("cd [ROOM NAME]                         Goes to room");
        Console.WriteLine();

    }
    // How it should look:
    /*
     * Day 'X' of 'MAX'
     * Turn left: 'X' of 'MAX'
     *
     * { RoomUiTemplate }
     * 
     *  general commands
     */
}