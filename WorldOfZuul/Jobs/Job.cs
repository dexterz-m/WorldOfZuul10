namespace WorldOfZuul.Jobs;

/// <summary>
/// Represents a type of job that can be assigned to <see cref="Villager"/> instances.
/// </summary>
/// <remarks>
/// Jobs have an identifier, a name and a description, and track villagers assigned to them.
/// The <see cref="Id"/> values are expected to follow the project's convention:
///0 - unemployed
///1 - lumberjack
///2 - farmer
/// </remarks>
/// <example>
/// // Create a lumberjack job and assign a villager:
/// var lumberjack = new Job(1, "Lumberjack", "Fells trees and gathers wood.");
/// lumberjack.AddVillager(villager);
/// lumberjack.Work();
/// </example>
public class Job
{
 public int Id { get; }
 /*
     * 0 - unemployed
     * 1 - lumberjack
     * 2 - farmer
 */
 public string Name { get; }
 public string Description { get; }

 /// <summary>
 /// List of villagers assigned to this job. May be null if no villagers are assigned.
 /// Use <see cref="AddVillager"/> to safely add a villager.
 /// </summary>
 public List<Villager>? Villagers { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Job"/> class.
    /// </summary>
    /// <param name="id">
    /// Job identifier (see remarks for conventions).
    /// 0 = unemployed, 1 = lumberjack, 2 = farmer.
    /// </param>
    /// <param name="name">Job name.</param>
    /// <param name="description">Job description.</param>
    public Job(int id, string name, string description)
 {
 Id = id;
 Name = name;
 Description = description;
 }

 /// <summary>
 /// Adds a villager to this job. Initializes the internal list if necessary.
 /// </summary>
 /// <param name="villager">The villager to assign to this job.</param>
 public void AddVillager(Villager villager)
 {
 Villagers ??= new List<Villager>();

 Villagers.Add(villager);
 }

 /// <summary>
 /// Performs the job-specific work. Override in derived job types to implement behaviour.
 /// </summary>
 public virtual void Work() { }
}