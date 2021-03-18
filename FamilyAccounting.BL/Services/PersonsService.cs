using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Interfaces;
using FamilyAccounting.DAL.Entities;
using System;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly IPersonsRepository personsRepository;
        private readonly IMapper mapper;
        public PersonsService(IPersonsRepository personsRepository, IMapper mapper)
        {
            this.personsRepository = personsRepository;
            this.mapper = mapper;
        }

        public PersonDTO Add(PersonDTO model)
        {
            try
            {
                Person person = personsRepository.Add(mapper.Map<Person>(model));
                return mapper.Map<PersonDTO>(person);
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
                IEnumerable<Person> person = personsRepository.GetListOfPersons();
                return mapper.Map<IEnumerable<PersonDTO>>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
