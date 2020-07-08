using AutoMapper;
using AutoInsurance.API.DTOs;
using AutoInsurance.API.Models;

namespace AutoInsurance.API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<CustomerDTO, Customer>()
            .ForMember(x => x.Image, options => options.Ignore());
            CreateMap<Vehicle, VehicleDTO>().ReverseMap();
            CreateMap<VehicleDTO, Vehicle>();
            CreateMap<Policy, PolicyDTO>().ReverseMap();
            CreateMap<PolicyDTO, Policy>();
            CreateMap<Coverage, CoverageDTO>().ReverseMap();
            CreateMap<CoverageDTO, Coverage>();
            CreateMap<Claim, ClaimDTO>().ReverseMap();
            CreateMap<ClaimDTO, Claim>();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<PaymentDTO, Payment>();
        }

    }

}

