﻿using MediatR;

namespace FliGen.Services.Tours.Application.Queries.TourById
{
    public class TourByIdQuery : IRequest<Dto.Tour>
    {
        public int TourId { get; }
        
        public TourByIdQuery(int tourId)
        {
            TourId = tourId;
        }
    }
}