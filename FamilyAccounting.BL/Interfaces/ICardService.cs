using FamilyAccounting.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.BL.Interfaces
{
    public interface ICardService
    {
        public CardDTO Create(CardDTO card);
    }
}
