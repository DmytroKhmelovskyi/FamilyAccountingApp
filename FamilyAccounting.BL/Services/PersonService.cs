using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;

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

        public PersonDTO Add(PersonDTO person)
        {
            try
            {
                Person _person = personsRepository.Add(mapper.Map<Person>(person));
                return mapper.Map<PersonDTO>(_person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PersonDTO Update(int id,PersonDTO person)
        {
            try
            {
                Person newPerson = mapper.Map<Person>(person);
                Person updatedPerson = personsRepository.Update(id, newPerson);
                return mapper.Map<PersonDTO>(updatedPerson);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<PersonDTO> Get()
        {
            try
            {
                IEnumerable<Person> person = personsRepository.Get();
                return mapper.Map<IEnumerable<PersonDTO>>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PersonDTO Get(int id)
        {
            Person person = personsRepository.Get(id);
            return mapper.Map<PersonDTO>(person);
        }

        public int Delete(int id)
        {
            try
            {
                return personsRepository.Delete(id);
                //return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<WalletDTO> GetWallets(int id)
        {
            try
            {
                IEnumerable<Wallet> wallets = personsRepository.GetWallets(id);
                return mapper.Map<IEnumerable<WalletDTO>>(wallets);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
