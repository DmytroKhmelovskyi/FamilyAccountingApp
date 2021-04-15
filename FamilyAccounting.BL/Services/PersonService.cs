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

        public async Task<PersonDTO> AddAsync(PersonDTO person)
        {
            return mapper.Map<PersonDTO>(await personsRepository.AddAsync(mapper.Map<Person>(person)));

        }
        public async Task<PersonDTO> UpdateAsync(int id, PersonDTO person)
        {
            Person newPerson = mapper.Map<Person>(person);
            return mapper.Map<PersonDTO>(await personsRepository.UpdateAsync(id, newPerson));

        }

        public async Task<IEnumerable<PersonDTO>> GetAsync()
        {
            IEnumerable<Person> person = await personsRepository.GetAsync();
            return mapper.Map<IEnumerable<PersonDTO>>(person);

        }

        public async Task<PersonDTO> GetAsync(int id)
        {
            return mapper.Map<PersonDTO>(await personsRepository.GetAsync(id));
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await personsRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<WalletDTO>> GetWalletsAsync(int id)
        {
            return mapper.Map<IEnumerable<WalletDTO>>(await personsRepository.GetWalletsAsync(id));
        }

        public PersonDTO Add(PersonDTO person)
        {
            return mapper.Map<PersonDTO>(personsRepository.Add(mapper.Map<Person>(person)));

        }
        public PersonDTO Update(int id, PersonDTO person)
        {
            Person newPerson = mapper.Map<Person>(person);
            return mapper.Map<PersonDTO>(personsRepository.Update(id, newPerson));

        }

        public IEnumerable<PersonDTO> Get()
        {
            IEnumerable<Person> person = personsRepository.Get();
            return mapper.Map<IEnumerable<PersonDTO>>(person);

        }

        public PersonDTO Get(int id)
        {
            return mapper.Map<PersonDTO>(personsRepository.Get(id));
        }

        public int Delete(int id)
        {
            return personsRepository.Delete(id);
        }

        public IEnumerable<WalletDTO> GetWallets(int id)
        {
            return mapper.Map<IEnumerable<WalletDTO>>(personsRepository.GetWallets(id));
        }
    }
}
