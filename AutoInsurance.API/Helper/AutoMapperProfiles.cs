using AutoMapper;
using AutoInsurance.API.DTOs;
using AutoInsurance.API.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

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
            CreateMap<VehicleCreationDTO, Vehicle>()
            .ForMember(x => x.VehicleCoverage, options => options.MapFrom(MapVehicleCoverage));
            CreateMap<Vehicle, VehicleDetailsDTO>()
              .ForMember(x => x.Coverage, options => options.MapFrom(MapVehicleCoverage));
            CreateMap<Policy, PolicyDTO>().ReverseMap();
            CreateMap<PolicyCreationDTO, Policy>()
            .ForMember(x => x.PolicyCoverage, options => options.MapFrom(MapPolicyCoverage));
            CreateMap<Policy, PolicyDetailsDTO>()
            .ForMember(x => x.Coverage, options => options.MapFrom(MapPolicyCoverage));

            CreateMap<Coverage, CoverageDTO>().ReverseMap();
            CreateMap<CoverageCreationDTO, Coverage>();
            CreateMap<CustClaim, CustClaimDTO>().ReverseMap();
            CreateMap<ClaimCreationDTO, CustClaim>();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<PaymentCreationDTO, Payment>();

             CreateMap<IdentityUser, UserDTO>()
                .ForMember(x => x.EmailAddress, options => options.MapFrom(x => x.Email))
                .ForMember(x => x.UserId, options => options.MapFrom(x => x.Id));
        }

        private List<CoverageDTO> MapPolicyCoverage(Policy policy, PolicyDetailsDTO policyDetailsDTO)
        {
            var result = new List<CoverageDTO>();
            foreach (var policycoverage in policy.PolicyCoverage)
            {
                result.Add(new CoverageDTO()
                {
                    Id = policycoverage.CoverageId,
                    Name = policycoverage.Coverage.Name,
                    Description = policycoverage.Coverage.Description,
                    isActiveCoverage = policycoverage.Coverage.isActiveCoverage
                });
            }
            return result;
        }

        private List<CoverageDTO> MapVehicleCoverage(Vehicle vehicle, VehicleDetailsDTO vehicleDetailsDTO)
        {
            var result = new List<CoverageDTO>();
            foreach (var vehiclecoverage in vehicle.VehicleCoverage)
            {
                result.Add(new CoverageDTO()
                {
                    Id = vehiclecoverage.CoverageId,
                    Name = vehiclecoverage.Coverage.Name,
                    Description = vehiclecoverage.Coverage.Description,
                    isActiveCoverage = vehiclecoverage.Coverage.isActiveCoverage
                });
            }
            return result;
        }

        private List<PolicyCoverage> MapPolicyCoverage(PolicyCreationDTO policyCreationDTO, Policy policy)
        {
            var result = new List<PolicyCoverage>();
            foreach (var id in policyCreationDTO.CoverageIds)
            {
                result.Add(new PolicyCoverage() { CoverageId = id });
            }
            return result;
        }
        private List<VehicleCoverage> MapVehicleCoverage(VehicleCreationDTO vehicleCreationDTO, Vehicle vehicle)
        {
            var result = new List<VehicleCoverage>();
            foreach (var id in vehicleCreationDTO.CoverageIds)
            {
                result.Add(new VehicleCoverage() { CoverageId = id });
            }
            return result;
        }
    }

}

