using AutoMapper;
using FamilyAccounting.BL.Interfaces;
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
    }
}
