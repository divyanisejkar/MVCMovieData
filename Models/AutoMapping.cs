using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace MovieData.Models
{
    public class AutoMapping :Profile
    {
        public AutoMapping()
        {
            CreateMap<User, RegisterRequestModel>();
            CreateMap<RegisterRequestModel, User>();

            CreateMap<MvcMovieContext, GetAllMoviesRequest>();
            CreateMap<GetAllMoviesRequest, MvcMovieContext>();

        }

    }
}
