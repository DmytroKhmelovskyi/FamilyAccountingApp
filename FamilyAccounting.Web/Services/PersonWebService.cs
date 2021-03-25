using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;

namespace FamilyAccounting.Web.Services
{
    public class PersonWebService : IPersonWebService
    {

        private readonly IPersonService personService;
        private readonly IMapper mapper;
        public PersonWebService(IMapper mapper, IPersonService personService)
        {
            this.personService = personService;
            this.mapper = mapper;
        }

        public PersonViewModel Add(PersonViewModel person)
        {
            try
            {
                PersonDTO _person = personService.Add(mapper.Map<PersonDTO>(person));
                return mapper.Map<PersonViewModel>(_person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PersonViewModel Update(int id, PersonViewModel person)
        {
            try
            {
                PersonDTO newPerson = mapper.Map<PersonDTO>(person);
                PersonDTO updatedPerson = personService.Update(id, newPerson);
                return mapper.Map<PersonViewModel>(updatedPerson);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<PersonViewModel> Get()
        {
            try
            {
                IEnumerable<PersonDTO> person = personService.Get();
                return mapper.Map<IEnumerable<PersonViewModel>>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PersonViewModel Get(int id)
        {
            PersonDTO person = personService.Get(id);
            return mapper.Map<PersonViewModel>(person);
        }

        public int Delete(int id)
        {
            try
            {
                return personService.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<WalletViewModel> GetWallets(int id)
        {
            try
            {
                IEnumerable<WalletDTO> wallets = personService.GetWallets(id);
                return mapper.Map<IEnumerable<WalletViewModel>>(wallets);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
