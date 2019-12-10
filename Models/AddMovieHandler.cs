using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace MovieData.Models
{
    public class AddMovieRequest:IRequest<AddMovieResponse>
    {
        [Required(ErrorMessage = "Enter Movie Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Movie Genre")]
        public string Genre { get; set; }

        [Range(1990, 2060)]
        public string ReleseDate { get; set; }

    }
    public class AddMovieResponse
    {
        public bool Success { get; set; }
    }
    internal class AddMovieHandler : IRequestHandler<AddMovieRequest, AddMovieResponse>
    {
        IMovieDataAccess _movieDataAccess;
        IMapper _mapper;
        public AddMovieHandler(IMovieDataAccess movieDataAccess,IMapper mapper)
        {
            _movieDataAccess = movieDataAccess;
            _mapper = mapper;
            

        }
        public async Task<AddMovieResponse> Handle(AddMovieRequest request, CancellationToken cancellationToken)
        {
            _movieDataAccess.AddMovie(_mapper.Map<MvcMovieContext>(request));
            return new AddMovieResponse
            {
                Success = true
            };
            
        }
    }
}
