using System;
using AutoInsurance.API.Validations;
using Microsoft.AspNetCore.Http;
using MoviesAPI.Validations;

namespace AutoInsurance.API.DTOs
{
    public class CustomerCreationDTO : CustomerPatchDTO
    {
        public int Id { get; set; }

        [FileSizeValidator(MaxFileSizeInMbs: 4)]
        [ContentTypeValidator(ContentTypeGroup.Image)]
        public IFormFile Image { get; set; }
    }
}