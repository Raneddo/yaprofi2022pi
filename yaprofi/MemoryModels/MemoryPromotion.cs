namespace yaprofi.MemoryModels;

public class MemoryPromotion
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public HashSet<int> Participants { get; } = new();
    public HashSet<int> Prizes { get; } = new();
}