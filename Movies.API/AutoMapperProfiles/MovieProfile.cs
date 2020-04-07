using AutoMapper;
using Movies.API.DTOs;
using Movies.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.AutoMapperProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieDTO, Entities.Movie>();
            CreateMap<Entities.Movie, Models.Movie>()
                .ForMember(dest=>dest.Director,opt=>opt.MapFrom(src=>
                                   $"{src.Director.FirstName} {src.Director.LastName}"));
        }
    }
}
