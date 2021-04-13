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
    public class AuditService : IAuditService
    {
        private readonly IMapper mapper;
        private IAuditRepository auditRepository;

        public AuditService(IMapper mapper, IAuditRepository auditRepository)
        {
            this.mapper = mapper;
            this.auditRepository = auditRepository;
        }

        public IEnumerable<AuditActionDTO> GetActions()
        {
            return mapper.Map<IEnumerable<AuditActionDTO>>(auditRepository.GetActions());
        }

        public IEnumerable<AuditWalletDTO> GetWallets()
        {
            return mapper.Map<IEnumerable<AuditWalletDTO>>(auditRepository.GetWallets());
        }

        public IEnumerable<AuditPersonDTO> GetPersons()
        {
            return mapper.Map<IEnumerable<AuditPersonDTO>>(auditRepository.GetPersons());
        }
    }
}
