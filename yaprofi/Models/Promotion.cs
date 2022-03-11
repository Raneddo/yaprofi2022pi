namespace yaprofi.Models;

public class Promotion
{
    /// <summary>
    /// идентификатор промоакции
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// название промоакции
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// описание промоакции
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// возможные призы в промоакции
    /// </summary>
    public List<Prize> Prizes { get; set; } = new();

    /// <summary>
    /// участники промоакции
    /// </summary>
    public List<Participant> Participants { get; set; } = new();
}