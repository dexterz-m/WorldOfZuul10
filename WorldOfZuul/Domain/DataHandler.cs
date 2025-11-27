using WorldOfZuul.Data;
using WorldOfZuul.Domain.Jobs;
using WorldOfZuul.Domain.Rooms;

namespace WorldOfZuul.Domain;

public class DataHandler
{
   public List<Room> Rooms { get; private set; }
   public List<Villager> Villagers { get; private set; }
   public Resources Resources { get; private set; }
   public Advisor Advisor { get; private set; }
   public List<Job> Jobs { get; private set; }
   public Room CurrentRoom { get; private set; }

   public DataHandler(IDataInitializer dataInitializer)
   {
      Resources = new Resources();
      Rooms = dataInitializer.LoadRooms();
      Villagers = dataInitializer.LoadVillagers();
      Advisor = dataInitializer.LoadAdvisor();
      Jobs = dataInitializer.LoadJobs();
      CurrentRoom = FindRoomByName("Village");
   }

   public void ChangeRoom(string roomName)
   {
      //CurrentRoom = FindRoomByName(string roomName)
      throw new NotImplementedException();
   }

   public Room FindRoomByName(string roomName)
   {
      throw new NotImplementedException();
     // Room room =  Rooms.Find(room => room.Name = roomName);
     // if (room is null) throw new Exception("Room not found!");
     // return room;
   }
}