using yaprofi.MemoryModels;

namespace yaprofi.Clients;

public class InMemoryPromoClient
{
    public IEnumerable<MemoryPromotion> GetAllPromotions()
    {
        return MemoryDb.Promotions.Select(x => x.Value);
    }

    public MemoryPromotion GetPromotionById(int id)
    {
        return MemoryDb.Promotions.TryGetValue(id, out var result) 
            ? result
            : null;
    }

    public IEnumerable<MemoryParticipant> GetAllParticipants()
    {
        return MemoryDb.Participants.Select(x => x.Value);
    }
    
    public IEnumerable<MemoryPrize> GetAllPrizes()
    {
        return MemoryDb.Prizes.Select(x => x.Value);
    }

    public MemoryParticipant GetParticipantById(int id)
    {
        return MemoryDb.Participants.TryGetValue(id, out var result) 
            ? result 
            : null;
    }

    public int CreatePromotion(string name, string description)
    {
        var id = MemoryDb.PromotionIndex;
        MemoryDb.IncPromotionIndex();
        
        MemoryDb.Promotions[id] = new MemoryPromotion()
        {
            Id = id,
            Name = name,
            Description = description
        };

        return id;
    }

    public void UpdatePromotion(int id, string name, string description)
    {
        if (!MemoryDb.Promotions.ContainsKey(id))
        {
            throw new ArgumentException("Promotion with id not found");
        }

        var promotion = MemoryDb.Promotions[id];

        promotion.Name = name;
        promotion.Description = description;
    }
    
    public void DeletePromotion(int id)
    {
        if (!MemoryDb.Promotions.ContainsKey(id))
        {
            throw new ArgumentException("Promotion with id not found");
        }

        MemoryDb.Promotions.Remove(id);
    }

    public int CreateParticipant(string name)
    {
        var id = MemoryDb.ParticipantIndex;
        MemoryDb.IncParticipantIndex();

        MemoryDb.Participants[id] = new MemoryParticipant()
        {
            Id = id,
            Name = name
        };

        return id;
    }

    public void AddParticipant(int promotionId, int participantId)
    {
        if (!MemoryDb.Promotions.ContainsKey(promotionId))
        {
            throw new ArgumentException("Promotion with id not found");
        }
        
        MemoryDb.Promotions[promotionId].Participants.Add(participantId);
    }

    public void DeleteParticipantFromPromotion(int promotionId, int participantId)
    {
        if (!MemoryDb.Promotions.ContainsKey(promotionId))
        {
            throw new ArgumentException("Promotion with id not found");
        }

        var promotion = MemoryDb.Promotions[promotionId];

        if (!promotion.Participants.Remove(participantId))
        {
            throw new ArgumentException("Participant with id not found");
        }
    }

    public void DeleteParticipant(int participantId)
    {
        if (!MemoryDb.Participants.Remove(participantId))
        {
            throw new ArgumentException("Participant with id not found");
        }
    }
    
    public MemoryPrize GetPrizeById(int id)
    {
        return MemoryDb.Prizes.TryGetValue(id, out var result) 
            ? result 
            : null;
    }

    public int CreatePrize(string description)
    {
        var id = MemoryDb.PrizeIndex;
        MemoryDb.IncPrizeIndex();

        MemoryDb.Prizes[id] = new MemoryPrize()
        {
            Id = id,
            Description = description
        };

        return id;
    }

    public void AddPrize(int promotionId, int prizeId)
    {
        if (!MemoryDb.Promotions.ContainsKey(promotionId))
        {
            throw new ArgumentException("Promotion with id not found");
        }
        
        MemoryDb.Promotions[promotionId].Prizes.Add(prizeId);
    }
    
    public void DeletePrizeFromPromotion(int promotionId, int prizeId)
    {
        if (!MemoryDb.Promotions.ContainsKey(promotionId))
        {
            throw new ArgumentException("Promotion with id not found");
        }

        var promotion = MemoryDb.Promotions[promotionId];

        if (!promotion.Prizes.Remove(prizeId))
        {
            throw new ArgumentException("Prize with id not found");
        }
    }

    public void DeletePrize(int prizeId)
    {
        if (!MemoryDb.Prizes.Remove(prizeId))
        {
            throw new ArgumentException("Prize with id not found");
        }
    }
}