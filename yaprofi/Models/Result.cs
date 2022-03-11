namespace yaprofi.Models;

public class Result
{
    /// <summary>
    /// победитель результатов розыгрыша
    /// </summary>
    public Participant Winner { get; set; }
    
    /// <summary>
    /// приз по результатам розыгрыша
    /// </summary>
    public Prize Prize { get; set; }
}