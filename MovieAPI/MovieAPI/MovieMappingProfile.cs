using AutoMapper;

using MovieAPI.DTOModels;
using MovieAPI.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI
{
    public class MovieMappingProfile : Profile
    {

        public MovieMappingProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(m => m.Id, d => d.MapFrom(s => s.Id))
                .ForMember(m => m.Drirector, d => d.MapFrom(s => s.Drirector))
                .ForMember(m => m.Title, d => d.MapFrom(s => s.Title));


            CreateMap<CreateOrEditMovieDto, Movie>()
                .ForMember(m => m.Drirector, d => d.MapFrom(s => s.Drirector))
                .ForMember(m => m.Title, d => d.MapFrom(s => s.Title));


            CreateMap<MovieDto, Movie>()
                .ForMember(m => m.Id, d => d.MapFrom(s => s.Id))
                .ForMember(m => m.Drirector, d => d.MapFrom(s => s.Drirector))
                .ForMember(m => m.Title, d => d.MapFrom(s => s.Title));
        }
    }
}
