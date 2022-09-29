using AutoMapper;
using Fincra.Models;

namespace Fincra
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePayoutVm, CreatePayout>().ReverseMap();
            CreateMap<Beneficiary, BeneficiaryDisbursmentNG>().ReverseMap();
            CreateMap<Beneficiary, BeneficiaryDisbursment>().ReverseMap();
            CreateMap<Beneficiary, BeneficiaryMobile>().ReverseMap();

        }
    }
}
