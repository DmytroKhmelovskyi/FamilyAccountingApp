using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;

namespace FamilyAccounting.Web.Services
{
    public class AuditWebService : IAuditWebService
    {
        private readonly IMapper mapper;
        private readonly IAuditService auditService;

        public AuditWebService(IMapper mapper, IAuditService auditService)
        {
            this.mapper = mapper;
            this.auditService = auditService;
        }
        public IEnumerable<AuditActionViewModel> GetActions()
        {
            try
            {
                IEnumerable<AuditActionDTO> auditAction = auditService.GetActions();
                return mapper.Map<IEnumerable<AuditActionViewModel>>(auditAction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<AuditWalletViewModel> GetWallets()
        {
            try
            {
                IEnumerable<AuditWalletDTO> auditWallet = auditService.GetWallets();
                return mapper.Map<IEnumerable<AuditWalletViewModel>>(auditWallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<AuditPersonViewModel> GetPersons()
        {
            try
            {
                IEnumerable<AuditPersonDTO> auditPerson = auditService.GetPersons();
                return mapper.Map<IEnumerable<AuditPersonViewModel>>(auditPerson);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
