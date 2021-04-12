using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personsRepository;
        private readonly IMapper mapper;
        public PersonService(IPersonRepository personsRepository, IMapper mapper)
        {
            this.personsRepository = personsRepository;
            this.mapper = mapper;
        }

        public async Task<PersonDTO> Add(PersonDTO person)
        {
            return mapper.Map<PersonDTO>(await personsRepository.Add(mapper.Map<Person>(person)));

        }
        public async Task<PersonDTO> Update(int id, PersonDTO person)
        {
            Person newPerson = mapper.Map<Person>(person);
            return mapper.Map<PersonDTO>(await personsRepository.Update(id, newPerson));

        }

        public async Task<IEnumerable<PersonDTO>> Get()
        {
            IEnumerable<Person> person = await personsRepository.Get();
            return mapper.Map<IEnumerable<PersonDTO>>(person);

        }

        public async Task<PersonDTO> Get(int id)
        {
            return mapper.Map<PersonDTO>(await personsRepository.Get(id));
        }

        public async Task<int> Delete(int id)
        {
            return await personsRepository.Delete(id);
        }

        public async Task<IEnumerable<WalletDTO>> GetWallets(int id)
        {
            return mapper.Map<IEnumerable<WalletDTO>>(await personsRepository.GetWallets(id));
        }
    }
}
