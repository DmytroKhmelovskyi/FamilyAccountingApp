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
            try
            {
                IEnumerable<AuditAction> auditAction = auditRepository.GetActions();
                return mapper.Map<IEnumerable<AuditActionDTO>>(auditAction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<AuditWalletDTO> GetWallets()
        {
            try
            {
                IEnumerable<AuditWallet> auditWallet = auditRepository.GetWallets();
                return mapper.Map<IEnumerable<AuditWalletDTO>>(auditWallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<AuditPersonDTO> GetPersons()
        {
            try
            {
                IEnumerable<AuditPerson> auditPerson = auditRepository.GetPersons();
                return mapper.Map<IEnumerable<AuditPersonDTO>>(auditPerson);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
