using WorldOfZuul.Domain;
using WorldOfZuul.Domain.Jobs;
using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.Data;

public class DataInitializer : IDataInitializer
{
    private readonly string _path;
   // private readonly StreamReader _streamReader;

    public List<Job> jobs { get; }
    public List<Room> rooms { get; }
    public List<Villager> villagers { get; }
    public Resources resources { get; }

    public DataInitializer()
    {

        _path = ResolveFromUpperDirs(Path.Combine("WorldOfZuul", "Data", "Database")); // adjust the path as necessary to working directory
        // because the working directory can be different based on where the application is run from


        jobs = LoadJobs();
        rooms = LoadRooms();
        villagers = LoadVillagers();
        resources = new Resources(); // default resources initialization might be changed later
    }
    private static string ResolveFromUpperDirs(string suffixRelativePath)
    {
        var dir = AppContext.BaseDirectory;

        while (dir is not null)
        {
            var candidate = Path.Combine(dir, suffixRelativePath);
            if (Directory.Exists(candidate))
                return candidate;

            dir = Directory.GetParent(dir)?.FullName;
        }

        throw new DirectoryNotFoundException(
            $"There is no: {suffixRelativePath} in any of the parent directories from {AppContext.BaseDirectory}");
    }

    private List<Job> LoadJobs()
    {
        string file = _path + "/Jobs.csv";
        if (!File.Exists(file)) throw new FileNotFoundException();
        
        StreamReader streamReader = new StreamReader(file);
        List<Job> jobs = new List<Job>();
        while (!streamReader.EndOfStream)
        {
            string? line = streamReader.ReadLine();
            if (line == null) continue;
            
            string[] splitString = line.Split(',');
            Job job = CreateJob(Convert.ToInt32(splitString[0]), splitString[1], splitString[2]);
            jobs.Add(job);
        }
        return jobs;
    }

    private Job CreateJob(int id, string name, string description)
    {
        return id switch
        {
            0 => new Unemployed(id, name, description),
            1 => new Hunter(id, name, description),
            2 => new Lumberjack(id, name, description),
            _ => throw new ArgumentException("Invalid job id")
        };
    }

    private List<Room> LoadRooms()
    {
        string file = _path + "/Rooms.csv";
        if (!File.Exists(file)) throw new FileNotFoundException();
        
        StreamReader streamReader = new StreamReader(file);
        List<Room> rooms = new List<Room>();
        while (!streamReader.EndOfStream)
        {
            string? line = streamReader.ReadLine();
            if (line == null) continue;
            
            string[] splitString = line.Split(',');
            Room room = CreateRoom(splitString[0], splitString[1]);
            rooms.Add(room);
        }
        return rooms;
    }

    private Room CreateRoom(string name, string description)
    {
        return name switch
        {
            "Village" => new Village(name, description),
            "Farmland" => new Farmland(name, description),
            "School" => new School(name, description),
            "Lake" => new Lake(name, description),
            "Forest" => new Forest(name, description),
            _ => throw new ArgumentException("Invalid room name")
        };
    }

    //private List<string> LoadCommands()
    //{
    //    string file = _path + "/commands.csv";
    //    if (!File.Exists(file)) throw new FileNotFoundException();
        
    //    StreamReader streamReader = new StreamReader(file);
    //    List<string> commands = new List<string>();
    //    while (!streamReader.EndOfStream)
    //    {
    //        string? line = streamReader.ReadLine();
    //        if (line == null) continue;
            
    //        string[] splitString = line.Split(',');
    //        foreach (string command in splitString)
    //        {
    //            commands.Add(command.Trim());
    //        }
    //    }
    //    return commands;
    //}


    private List<Villager> LoadVillagers()
    {
        string file = _path + "/Villagers.csv";
        if (!File.Exists(file)) throw new FileNotFoundException();
        
        StreamReader streamReader = new StreamReader(file);
        List<Villager> villagers = new List<Villager>();
        while (!streamReader.EndOfStream)
        {
            string? line = streamReader.ReadLine();
            if (line == null) continue;
            
            string[] splitString = line.Split(',');
            Villager villager = new  Villager(Convert.ToInt32(splitString[0]), splitString[1]);
            villagers.Add(villager);
        }
        return villagers;
    }
}