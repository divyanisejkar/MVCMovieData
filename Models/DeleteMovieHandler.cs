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
    public class DeleteMovieRequest:IRequest<DeleteMovieResponse>
    {
        public int? Id { get; set; }
        
        

    }
    public class DeleteMovieResponse
    {
        public bool success { get; set; }
    }
    internal class DeleteMovieHandler : IRequestHandler<DeleteMovieRequest, DeleteMovieResponse>
    {
        IMovieDataAccess _movieDataAccess;
       // private readonly IMapper _mapper;
        public DeleteMovieHandler(IMovieDataAccess movieDataAccess)
        {
            _movieDataAccess = movieDataAccess;
           // _mapper = mapper;
        }
        public async Task<DeleteMovieResponse> Handle(DeleteMovieRequest request, CancellationToken cancellationToken)
        {
            _movieDataAccess.DeleteMovie(request.Id);
            return new DeleteMovieResponse
            {
                success = true
            };
        }
    }
}
