using FliGen.Services.Tours.Application.Dto;
using MediatR;
using System.Collections.Generic;

namespace FliGen.Services.Tours.Application.Queries.RegisteredOnTourPlayers
{
    public class RegisteredOnTourPlayers : IRequest<IEnumerable<PlayerInternalIdDto>>
    {
        public int TourId { get; set; }
    }
}