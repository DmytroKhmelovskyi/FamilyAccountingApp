using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Interfaces;
using FamilyAccounting.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.BL.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly IPersonsRepository personsRepository;
        public PersonsService(IPersonsRepository personsRepository)
        {
            this.personsRepository = personsRepository;
        }

        public PersonDTO Add(PersonDTO person)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDTO>());
                var mapper = new Mapper(config);
                Person addedPerson = personsRepository.Add(mapper.Map<Person>(person));

                return person;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<PersonDTO> GetListOfPersons()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<PersonDTO>> (personsRepository.GetListOfPersons());
        }
    }
}
