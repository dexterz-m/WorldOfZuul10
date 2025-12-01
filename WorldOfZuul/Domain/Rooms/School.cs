using WorldOfZuul.Domain.CommandHandler;

namespace WorldOfZuul.Domain.Rooms;

public class School : Room
{
    public School(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {
    }

    public override string GetEnterRoomMessage()
    {
        return "You have entered the School. Learn about sustainability here.";
    }

    public string GetSchoolInfo(int sustainabilityPoints)
    {
        return $"Sustainability Points: {sustainabilityPoints}\n\n" +
               "Available actions:\n" +
               "about : About the project\n" +
               "learn : Learn about sustainability\n\n" +
               "Type a command to perform the action.\n";
    }

    public string GetAboutProject(int sustainabilityPoints)
    {
        return "About this project:\n" +
               "This is a learning game demonstrating simple resource and villager management.\n" +
               "Make choices that affect sustainability points and the world state.\n\n" +
               "Purpose:\n" +
               "- The game is educational: it illustrates consequences of consumption and resource use.\n" +
               "- Players explore trade-offs between short-term gains and long-term sustainability.\n" +
               "- It connects simple game mechanics to the UN Sustainable Development Goals to raise awareness.\n\n" +
               "Sustainability basics:\n" +
               "- Sustainable actions increase sustainability points and help preserve resources.\n" +
               "- Unsustainable actions decrease sustainability points and can lead to resource depletion.\n" +
               $"Current Sustainability Points: {sustainabilityPoints}\n";
    }

    public string LearnSustainability()
    {
        return "According to the SDG report of 2025, one of the main sustainability problems is overconsumption " +
               "which will be the main focus of the game.The challenge we are faced with is to ensure sustainable " +
               "consumption and production patterns, either in the food industry, in the retail industry or just when " +
               "using natural resources.";
    }
}