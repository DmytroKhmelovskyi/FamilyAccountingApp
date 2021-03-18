using AutoMapper;
using FamilyAccounting.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.BL.Services
{

    public class CardsService
    {
        private readonly IMapper mapper;
        private ICardsService cardsService;
        public CardsService(IMapper mapper, ICardsService cardsService)
        {
            this.cardsService = cardsService;
            this.mapper = mapper;

        }
    }
}
