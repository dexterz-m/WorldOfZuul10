namespace WorldOfZuul.Jobs;

/// <summary>
/// Represents the unemployed job state for a villager.
/// </summary>
/// <remarks>
/// Use <see cref="Unemployed"/> to mark villagers that do not perform work and do not
/// produce resources. This is the job with Id = 0 according to the project's job id
/// conventions (0 = unemployed, 1 = lumberjack, 2 = farmer).
/// 
/// Typically a villager will be moved to this job when they are not assigned to any
/// productive task. The <see cref="Job.Villagers"/> collection may be empty or null when
/// no villagers are unemployed.
/// </remarks>
/// <example>
/// // Mark a villager as unemployed:
/// var unemployed = new Unemployed();
/// unemployed.AddVillager(villager);
/// // No Work() behaviour - calling Work() has no effect by default
/// unemployed.Work();
/// </example>
public class Unemployed : Job
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Unemployed"/> job.
    /// </summary>
    public Unemployed() : base(0, "Unemployed", "")
    {
        
    }
}