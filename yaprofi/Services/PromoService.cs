using yaprofi.Clients;
using yaprofi.Models;

namespace yaprofi.Services;

public class PromoService
{
    private readonly InMemoryPromoClient _promoClient;
    private readonly Random _random = new(DateTime.Now.Millisecond);

    public PromoService(InMemoryPromoClient promoClient)
    {
        _promoClient = promoClient;
    }

    public IEnumerable<Promotion> GetAllPromotions()
    {
        var promotions = _promoClient.GetAllPromotions()
            .Select(x => new Promotion
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Participants = x.Participants.Select(p => new Participant()
                {
                    Id = p,
                    Name = _promoClient.GetParticipantById(p).Name
                }).ToList(),
                Prizes = x.Prizes.Select(p => new Prize()
                {
                    Id = p,
                    Description = _promoClient.GetPrizeById(p).Description
                }).ToList()
            });
        
        return promotions;
    }

    public Promotion GetPromotion(int id)
    {
        var memoryPromotion = _promoClient.GetPromotionById(id);
        if (memoryPromotion == null)
        {
            return null;
        }
        
        return new Promotion
        {
            Id = memoryPromotion.Id,
            Name = memoryPromotion.Name,
            Description = memoryPromotion.Description,
            Participants = memoryPromotion.Participants.Select(p => new Participant()
            {
                Id = p,
                Name = _promoClient.GetParticipantById(p).Name
            }).ToList(),
            Prizes = memoryPromotion.Prizes.Select(p => new Prize()
            {
                Id = p,
                Description = _promoClient.GetPrizeById(p).Description
            }).ToList()
        };
    }

    public int CreatePromotion(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        return _promoClient.CreatePromotion(name, description);
    }

    public void UpdatePromotion(int id, string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        _promoClient.UpdatePromotion(id, name, description);
    }

    public void DeletePromotion(int id)
    {
        _promoClient.DeletePromotion(id);
    }

    public int CreateParticipant(int promotionId, string name)
    {
        var participantId = _promoClient.CreateParticipant(name);
        _promoClient.AddParticipant(promotionId, participantId);

        return participantId;
    }

    public void DeleteParticipant(int promotionId, int participantId)
    {
        _promoClient.DeleteParticipantFromPromotion(promotionId, participantId);
        _promoClient.DeleteParticipant(participantId);
    }

    public int CreatePrize(int promotionId, string description)
    {
        var prizeId = _promoClient.CreatePrize(description);
        _promoClient.AddPrize(promotionId, prizeId);

        return prizeId;
    }
    
    public void DeletePrize(int promotionId, int prizeId)
    {
        _promoClient.DeletePrizeFromPromotion(promotionId, prizeId);
        _promoClient.DeletePrize(prizeId);
    }

    public IEnumerable<Result> Raffle(int promotionId)
    {
        var promotion = _promoClient.GetPromotionById(promotionId);
        var participants = promotion.Participants
            .Select(x =>
            {
                var memoryParticipant = _promoClient.GetParticipantById(x);
                return new Participant()
                {
                    Id = memoryParticipant.Id,
                    Name = memoryParticipant.Name
                };
            })
            .OrderBy(x => _random.Next())
            .ToList();
        
        var prizes = promotion.Prizes
            .Select(x =>
            {
                var memoryPrize = _promoClient.GetPrizeById(x);
                return new Prize()
                {
                    Id = memoryPrize.Id,
                    Description = memoryPrize.Description
                };
            })
            .OrderBy(x => _random.Next())
            .ToList();

        if (participants.Count != prizes.Count)
        {
            throw new ArgumentException("Can't raffle given promotion");
        }

        var winners = participants.Zip(prizes)
            .Select(x => new Result()
            {
                Winner = x.First,
                Prize = x.Second
            });

        return winners;
    }
}