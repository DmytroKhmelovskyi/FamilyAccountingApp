using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;

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
        public CardDTO Create(CardDTO card)
        {
            try
            {
                Card _card = cardsRepository.Create(mapper.Map<Card>(card));
                return mapper.Map<CardDTO>(_card);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int Delete(int id)
        {
            try
            {
                return cardsRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CardDTO Get(int id)
        {
            Card card = cardsRepository.Get(id);
            return mapper.Map<CardDTO>(card);
        }

        public CardDTO Update(int id, CardDTO card)
        {
            Card newCard = mapper.Map<Card>(card);
            Card updatedCard = cardsRepository.Update(id, newCard);
            return mapper.Map<CardDTO>(updatedCard);
        }

    }
}
