using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
