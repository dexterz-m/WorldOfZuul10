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
        
        Console.WriteLine($"{dh.CurrentRoom?.LongDescription}");

        switch (dh.CurrentRoom?.ShortDescription)
        {
            case "Village":
                Console.WriteLine("Below are the current stats:");
                Console.WriteLine($"Villagers: {dh.Villagers}");
                Console.WriteLine();
                
                Console.WriteLine("Here are available commands");
                Console.WriteLine("ls v                                   List out all villagers and their status");
                Console.WriteLine("ls r                                   List out all rooms");
                Console.WriteLine("ls j                                   List out all jobs");
                Console.WriteLine("cd [ROOM NAME]                         Goes to room");
                Console.WriteLine();
                Console.WriteLine("feed -[VILLAGER ID] [AMOUNT/DAY]       Feeds villager and activates it");
                Console.WriteLine("assign -[VILLAGER ID] [JOB NAME]       Assigns villager to a task");
                Console.WriteLine("sleep                                  Skip the remaining moves");
                break;
            
            case "School":
                Console.WriteLine("You have entered the School. Learn about sustainability here.");
                Console.WriteLine();

               
                Console.WriteLine($"Sustainability Points: {Game.SustainabilityPoints}");
                Console.WriteLine();

                Console.WriteLine("Available actions:");

                Console.WriteLine("about : About the project");
                Console.WriteLine("learn : Learn about sustainability");
                Console.WriteLine();
                Console.WriteLine("Type a command to perform the action.");
                Console.WriteLine();
                break;
            
            case "Lake":
                Console.WriteLine("You have arrived at the lake. Below are the current stats:");
                Console.WriteLine($"Fish in the lake: {dh.fish}");
                Console.WriteLine($"Grain seeds in your pocket: {dh.Resources.GrainSeeds}");

                Console.WriteLine("Available actions:");
                Console.WriteLine(" catch fish - start fishing  : Catch fish");
                Console.WriteLine(" feed fish  - feed fish      : Feed fish");
                Console.WriteLine();
                Console.WriteLine("Type a command to perform the action.");
                Console.WriteLine();
                break;
            
            case "Forest":
                Console.WriteLine("Below are the current stats:");
                Console.WriteLine($"Trees: {dh.Resources.Trees}");
                Console.WriteLine($"Animals: {dh.Resources.Animals}");
                Console.WriteLine($"Sustainability Points: {Game.SustainabilityPoints}");
                Console.WriteLine();
                
                Console.WriteLine("Available actions:");
                Console.WriteLine("cut - cut tree    : Cut down one tree (reduces sustainability)");
                Console.WriteLine("plant - plant tree  : Plant a tree (increases sustainability)");
                Console.WriteLine("kill - kill animal : Kill one animal (reduces sustainability)");
                Console.WriteLine();


                Console.WriteLine("Type a command to perform the action.");
                Console.WriteLine();
                break;
            
            case "Farmland":
                Console.WriteLine("You have entered the Farmland. Below are the current stats:");
                Console.WriteLine($"Farmlands: {dh.FarmlandAmount}");
                Console.WriteLine($"Free farmlands: {dh.PossibleFarmland - dh.FarmlandAmount}");
                Console.WriteLine();

                Console.WriteLine("Available actions:");
                Console.WriteLine(" - build farmland           : Build a new farmland");
                Console.WriteLine(" - cut forest               : Cut 5 trees to make freeland (reduces sustainability)");
                Console.WriteLine(" - plant farmland           : Plant on your farmland");
                Console.WriteLine(" - farm                     : Farm your planted farmland");
                Console.WriteLine();
                Console.WriteLine("Type a command to perform the action.");
                Console.WriteLine();
                break;
            
            default:

                Console.WriteLine("You got lost. You are back in the village now!!");
                Console.WriteLine();
                
                Console.WriteLine("Below are the current stats:");
                Console.WriteLine($"Villagers: {dh.Villagers}");
                Console.WriteLine();
                
                Console.WriteLine("Here are available commands");
                Console.WriteLine("ls v                                   List out all villagers and their status");
                Console.WriteLine("ls r                                   List out all rooms");
                Console.WriteLine("ls j                                   List out all jobs");
                Console.WriteLine("cd [ROOM NAME]                         Goes to room");
                Console.WriteLine();
                Console.WriteLine("feed -[VILLAGER ID] [AMOUNT/DAY]       Feeds villager and activates it");
                Console.WriteLine("assign -[VILLAGER ID] [JOB NAME]       Assigns villager to a task");
                Console.WriteLine("sleep                                  Skip the remaining moves");
                
                break;
        }
        
        
    }
}