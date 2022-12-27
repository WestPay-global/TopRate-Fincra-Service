using AutoMapper;
using Fincra.Models.Dtos.Request;
using Fincra.Models.ThirdParty.Request;

namespace Fincra
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Dtos.Request.Beneficiary, BankAccountBeneficiary>();
            CreateMap<Models.Dtos.Request.Beneficiary, MobileMoneyBeneficiary>();
            CreateMap<Payout, PayoutRequest>()
                .ForMember(destinationMember => destinationMember.DestinationCurrecy, paymentDestination => paymentDestination.MapFrom(src => src.DestinationCurrecy.ToString().ToLower()))
                .ForMember(destinationMember => destinationMember.PaymentDestination, paymentDestination => paymentDestination.MapFrom(src => src.PaymentDestination.ToString().ToLower()))
                .ForMember(destinationMember => destinationMember.Beneficiary, output => output.Ignore());

            CreateMap<Payout, GenerateQuote>()
              .ForMember(destinationMember => destinationMember.PaymentDestination, paymentDestination => paymentDestination.MapFrom(src => src.PaymentDestination.ToString().ToLower()));
            CreateMap<Payout, TransferRequest>();
        }
    }
}
