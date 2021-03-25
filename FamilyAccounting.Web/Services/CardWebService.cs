using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using System;

namespace FamilyAccounting.Web.Services
{
    public class CardWebService : ICardWebService
    {
        private readonly IMapper mapper;
        private readonly ICardService cardService;

        public CardWebService(IMapper mapper, ICardService cardService)
        {
            this.mapper = mapper;
            this.cardService = cardService;
        }
        public CardViewModel Create(CardViewModel card)
        {
            try
            {
                CardDTO _card = cardService.Create(mapper.Map<CardDTO>(card));
                return mapper.Map<CardViewModel>(_card);
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
                return cardService.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CardViewModel Get(int id)
        {
            CardDTO card = cardService.Get(id);
            return mapper.Map<CardViewModel>(card);
        }

        public CardViewModel Update(int id, CardViewModel card)
        {
            CardDTO newCard = mapper.Map<CardDTO>(card);
            CardDTO updatedCard = cardService.Update(id, newCard);
            return mapper.Map<CardViewModel>(updatedCard);
        }

    }
}
