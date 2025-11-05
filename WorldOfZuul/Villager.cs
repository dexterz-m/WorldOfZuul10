namespace WorldOfZuul
{
    public class Villager
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Villager(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
