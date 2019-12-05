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
    public class GetMovieDataRequest:IRequest<GetMovieDataResponse>
    {
        public int? Id { get; set; }
        
    }
    public class GetMovieDataResponse
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Movie Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Movie Genre")]
        public string Genre { get; set; }

        [Range(1990, 2060)]

        public string ReleseDate { get; set; }
    }
    internal class GetMovieDataHandler : IRequestHandler<GetMovieDataRequest, GetMovieDataResponse>
    {
        IMovieDataAccess _movieDataAccess;
        IMapper _mapper;
        public GetMovieDataHandler(IMovieDataAccess movieDataAccess, IMapper mapper)
        {
            _movieDataAccess = movieDataAccess;
            _mapper = mapper;
        }

        public async Task<GetMovieDataResponse> Handle(GetMovieDataRequest request, CancellationToken cancellationToken)
        {
            var movie = _mapper.Map<GetMovieDataResponse>(_movieDataAccess.GetMovieData(request.Id));
            return movie;
        }
    }
}
