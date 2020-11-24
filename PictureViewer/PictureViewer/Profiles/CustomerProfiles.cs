using AutoMapper;
using Core.Models;
using Core.Models.Input;
using Core.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureViewer.Profiles
{
    public class CustomerProfiles : Profile
    {
        public CustomerProfiles()
        {
            CreateMap<Customer, CustomerOutputDto>();

            CreateMap<CustomerInputDto, Customer>();
        }
    }
}
