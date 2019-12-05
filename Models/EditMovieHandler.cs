using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace MovieData.Models
{
    public class EditMovieRequest :IRequest<EditMovieRsponse>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Movie Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter Movie Genre")]
        public string Genre { get; set; }

        [Range(1990, 2060)]

        public string ReleseDate { get; set; }

    }
    public class EditMovieRsponse
    {
        public bool Success{ get; set; }

    }
    internal class EditMovieHandler : IRequestHandler<EditMovieRequest, EditMovieRsponse>
    {
      private readonly  IMovieDataAccess _movieDataAccess;
        //IMediator _mediator;
      private readonly  IMapper _mapper;
        public EditMovieHandler(IMovieDataAccess movieDataAccess,IMapper mapper)
        {
            _movieDataAccess = movieDataAccess;
            _mapper = mapper;
           // _mediator = mediator;
        }
        public async Task<EditMovieRsponse> Handle(EditMovieRequest request, CancellationToken cancellationToken)
        {
            _movieDataAccess.UpdateMovie(_mapper.Map<MvcMovieContext>(request));
            return new EditMovieRsponse
            {
                Success = true
            };

        }
    }
}
