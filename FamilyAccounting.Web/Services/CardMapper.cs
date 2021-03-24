using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Services
{
    public class CardMapper
    {
        public static CardDTO CardMap(CardViewModel cardVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CardViewModel, CardDTO>());
            var mapper = new Mapper(config);
            var cardDTO = mapper.Map<CardDTO>(cardVM);
            return cardDTO;
        }

        public static CardViewModel CardMap(CardDTO cardVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CardDTO, CardViewModel>());
            var mapper = new Mapper(config);
            var cardDTO = mapper.Map<CardViewModel>(cardVM);
            return cardDTO;
        }
    }
}
