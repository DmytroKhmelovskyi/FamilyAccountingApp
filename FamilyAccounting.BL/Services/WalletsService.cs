using AutoMapper;
using FamilyAccounting.BL.Interfaces;

namespace FamilyAccounting.BL.Services
{

    class WalletsService
    {
        private readonly IMapper mapper;
        private IWalletsService walletsService;
        public WalletsService(IMapper mapper, IWalletsService walletsService)
        {
            this.mapper = mapper;
            this.walletsService = walletsService;
        }

    }
}
