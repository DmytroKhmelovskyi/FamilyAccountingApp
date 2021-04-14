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
        public async Task<CardDTO> CreateAsync(CardDTO card)
        {
            return mapper.Map<CardDTO>(await cardsRepository.CreateAsync(mapper.Map<Card>(card)));
        }
        public async Task<int> DeleteAsync(int id)
        {

            return await cardsRepository.DeleteAsync(id);
        }

        public async Task<CardDTO> GetAsync(int id)
        {
            return mapper.Map<CardDTO>(await cardsRepository.GetAsync(id));
        }

        public async Task<CardDTO> UpdateAsync(int id, CardDTO card)
        {
            Card newCard = mapper.Map<Card>(card);
            return mapper.Map<CardDTO>(await cardsRepository.UpdateAsync(id, newCard));
        }

        public CardDTO Create(CardDTO card)
        {
            return mapper.Map<CardDTO>(cardsRepository.Create(mapper.Map<Card>(card)));
        }
        public int Delete(int id)
        {

            return cardsRepository.Delete(id);
        }

        public CardDTO Get(int id)
        {
            return mapper.Map<CardDTO>(cardsRepository.Get(id));
        }

        public CardDTO Update(int id, CardDTO card)
        {
            Card newCard = mapper.Map<Card>(card);
            return mapper.Map<CardDTO>(cardsRepository.Update(id, newCard));
        }
    }
}
