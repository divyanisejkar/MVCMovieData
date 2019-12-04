using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace MovieData.Models
{
    public class GetAllMoviesRequest :IRequest<GetAllMoviesResponse>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Movie Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Movie Genre")]
        public string Genre { get; set; }

        [Range(1990, 2060)]

        public string ReleseDate { get; set; }
    }
    public class GetAllMoviesResponse 
    {
        public List<MvcMovieContext> Success { get; set; }
    }
    internal class GetAllMoviesHandler : IRequestHandler<GetAllMoviesRequest, GetAllMoviesResponse>
    {
        IMovieDataAccess _movieDataAccess;
        private readonly IMapper _mapper;
        public GetAllMoviesHandler(IMovieDataAccess movieDataAccess,IMapper mapper)
        {
            _movieDataAccess = movieDataAccess;
            _mapper = mapper;

        }
        public async Task<GetAllMoviesResponse> Handle(GetAllMoviesRequest request, CancellationToken cancellationToken)
        {
            return new GetAllMoviesResponse()
            {
                Success = _movieDataAccess.GetAllMovies()

            };
        }
    }
}
