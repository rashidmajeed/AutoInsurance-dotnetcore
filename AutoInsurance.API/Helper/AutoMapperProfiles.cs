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
            CreateMap<CustomerCreationDTO, Customer>()
            .ForMember(x => x.Image, options => options.Ignore());
            CreateMap<Customer, CustomerPatchDTO>().ReverseMap();
            CreateMap<Vehicle, VehicleDTO>().ReverseMap();
            CreateMap<VehicleCreationDTO, Vehicle>();
            CreateMap<Policy, PolicyDTO>().ReverseMap();
            CreateMap<PolicyCreationDTO, Policy>();
            CreateMap<Coverage, CoverageDTO>().ReverseMap();
            CreateMap<CoverageCreationDTO, Coverage>();
            CreateMap<Claim, ClaimDTO>().ReverseMap();
            CreateMap<ClaimCreationDTO, Claim>();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<PaymentCreationDTO, Payment>();
        }

    }

}

