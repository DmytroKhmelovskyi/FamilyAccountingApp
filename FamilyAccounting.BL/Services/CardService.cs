using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Services
{

    public class CardService : ICardService
    {
        private readonly IMapper mapper;
        private ICardRepository cardsRepository;

        public CardService(IMapper mapper, ICardRepository cardsRepository)
        {
            this.mapper = mapper;
            this.cardsRepository = cardsRepository;
        }
        public async Task<CardDTO> Create(CardDTO card)
        {
            return mapper.Map<CardDTO>(await cardsRepository.Create(mapper.Map<Card>(card)));
        }
        public async Task<int> Delete(int id)
        {

            return await cardsRepository.Delete(id);
        }

        public async Task<CardDTO> Get(int id)
        {
            return mapper.Map<CardDTO>(await cardsRepository.Get(id));
        }

        public async Task<CardDTO> Update(int id, CardDTO card)
        {
            Card newCard = mapper.Map<Card>(card);
            return mapper.Map<CardDTO>(await cardsRepository.Update(id, newCard));
        }

    }
}
