using WorldOfZuul.Domain;
using WorldOfZuul.Domain.Jobs;
using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.Data;

public class DataInitializer : IDataInitializer
{
    private readonly string _path;
    private readonly StreamReader _streamReader;

    public DataInitializer(string path)
    {
        _path = path;
    }
    public List<Job> LoadJobs()
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
            Job job = CreateJob(Convert.ToInt32(splitString[0]), splitString[1]);
            jobs.Add(job);
        }
        return jobs;
    }

    private Job CreateJob(int id, string name)
    {
        throw new NotImplementedException();
       // return id switch
       // {
       //     0 => new Unemployed(id, name),
       //     1 => new Hunter(id, name),
       //     2 => new Lumberjack(id, name)
       // };
    }

    public List<Room> LoadRooms()
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
        throw new NotImplementedException();
    }

    public List<string> LoadCommands()
    {
        string file = _path + "/commands.csv";
        if (!File.Exists(file)) throw new FileNotFoundException();
        
        StreamReader streamReader = new StreamReader(file);
        List<string> commands = new List<string>();
        while (!streamReader.EndOfStream)
        {
            string? line = streamReader.ReadLine();
            if (line == null) continue;
            
            string[] splitString = line.Split(',');
            foreach (string command in splitString)
            {
                commands.Add(command.Trim());
            }
        }
        return commands;
    }
    

    public List<Villager> LoadVillagers()
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
            //Villager villager = new  Villager(Convert.ToInt32(splitString[0]), splitString[1], Convert.ToInt32(splitString[2]));
            Villager villager = new Villager();
            villagers.Add(villager);
        }
        return villagers;
    }

    public Advisor LoadAdvisor()
    {
        string file = _path + "/Jobs.csv";
        if (!File.Exists(file)) throw new FileNotFoundException();
        
        StreamReader streamReader = new StreamReader(file);
        Advisor advisor = new Advisor();
        while (!streamReader.EndOfStream)
        {
            string? line = streamReader.ReadLine();
            if (line == null) continue;
            string[] splitString = line.Split(',');
            
            //TODO: read advisor
        }

        throw new NotImplementedException();
        return advisor;
    }
}