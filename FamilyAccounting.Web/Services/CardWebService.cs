using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using System;
using System.Threading.Tasks;

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
        public async Task<CardViewModel> Create(CardViewModel card)
        {
            try
            {
                CardDTO _card = await cardService.CreateAsync(mapper.Map<CardDTO>(card));
                return mapper.Map<CardViewModel>(_card);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> Delete(int id)
        {
            try
            {
                return await cardService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CardViewModel> Get(int id)
        {
            CardDTO card = await cardService.GetAsync(id);
            return mapper.Map<CardViewModel>(card);
        }

        public async Task<CardViewModel> Update(int id, CardViewModel card)
        {
            CardDTO newCard = mapper.Map<CardDTO>(card);
            CardDTO updatedCard = await cardService.UpdateAsync(id, newCard);
            return mapper.Map<CardViewModel>(updatedCard);
        }

    }
}
