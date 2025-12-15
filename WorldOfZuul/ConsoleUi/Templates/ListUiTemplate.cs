using WorldOfZuul.Data;
using WorldOfZuul.Domain;
using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.ConsoleUi.Templates;

public class ListUiTemplate
{
    private DataHandler handler;
    public ListUiTemplate(DataHandler dataHandler)
    {
        handler = dataHandler;
    }

    public void List(char? type)
    {
        
        switch (type)
        {
            case 'v':
                if (handler.Villagers != null)
                    foreach (Villager villager in handler.Villagers)
                    {
                        //Console.WriteLine($"{villager.Id} | {villager.Name}");
                    }

                break;
            case 'j':
                Console.WriteLine("Jobs");
                break;
            case 'r':
                foreach (Room roomName in handler.Rooms)
                {
                    Console.WriteLine(roomName.ShortDescription);
                }
                break;
            case 'i':
                Console.WriteLine($"Food : {handler.Resources.Food}");
                Console.WriteLine($"Hunger : {handler.Resources.Hunger}");
                Console.WriteLine($"Saplings : {handler.Resources.Saplings}");
                Console.WriteLine($"Animals : {handler.Resources.Animals}");
                Console.WriteLine($"GrainSeeds : {handler.Resources.GrainSeeds}");
                Console.WriteLine($"Trees : {handler.Resources.Trees}");
                Console.WriteLine($"Wood : {handler.Resources.Wood}");
                break;
            default:
                Console.WriteLine("Wrong command! Try 'help' to see syntax.");
                break;
        }
    }
    // generic template for listing villagers, resources, rooms, commands, and jobs 
    // Called in game.cs
}