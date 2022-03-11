
using yaprofi.MemoryModels;

namespace yaprofi;

public static class MemoryDb
{
    public static int PromotionIndex { get; private set; } = 1;
    public static int ParticipantIndex { get; private set; } = 1;
    public static int PrizeIndex { get; private set; } = 1;
    public static Dictionary<int, MemoryPromotion> Promotions { get; } = new();
    public static Dictionary<int, MemoryParticipant> Participants { get; } = new();
    public static Dictionary<int, MemoryPrize> Prizes { get; } = new();

    public static void IncPromotionIndex()
    {
        PromotionIndex++;
    }
    
    public static void IncParticipantIndex()
    {
        ParticipantIndex++;
    }
    
    public static void IncPrizeIndex()
    {
        PrizeIndex++;
    }
}