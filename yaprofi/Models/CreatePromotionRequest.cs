namespace yaprofi.Models;

public class CreatePromotionRequest
{
    /// <summary>
    /// название промоакции
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// описание промоакции
    /// </summary>
    public string Description { get; set; }
}