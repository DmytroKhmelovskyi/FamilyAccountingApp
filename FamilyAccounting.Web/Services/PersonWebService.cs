using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<PersonViewModel> Add(PersonViewModel person)
        {
                PersonDTO _person = await personService.Add(mapper.Map<PersonDTO>(person));
                return mapper.Map<PersonViewModel>(_person);
        }
        public async Task<PersonViewModel> Update(int id, PersonViewModel person)
        {
            try
            {
                PersonDTO newPerson = mapper.Map<PersonDTO>(person);
                PersonDTO updatedPerson = await personService.Update(id, newPerson);
                return mapper.Map<PersonViewModel>(updatedPerson);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PersonViewModel>> Get()
        {
            try
            {
                IEnumerable<PersonDTO> person = await personService.Get();
                return mapper.Map<IEnumerable<PersonViewModel>>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PersonViewModel> Get(int id)
        {
            PersonDTO person = await personService.Get(id);
            return mapper.Map<PersonViewModel>(person);
        }

        public async Task<int> Delete(int id)
        {
                return await personService.Delete(id);
        }

        public async Task<IEnumerable<WalletViewModel>> GetWallets(int id)
        {
                IEnumerable<WalletDTO> wallets = await personService.GetWallets(id);
                return mapper.Map<IEnumerable<WalletViewModel>>(wallets);
        }
    }
}
