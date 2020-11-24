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
    public class PictureProfiles : Profile
    {
        public PictureProfiles()
        {
            CreateMap<Picture, PictureOutputDto>()
                .ForMember(dest => dest.Uploader, opt => opt.MapFrom(src => src.Uploader.Username));

            CreateMap<PictureInputDto, Picture>();
        }
    }
}
