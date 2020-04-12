﻿using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Operations.Messages.Tours.Events
{
    [MessageNamespace("tours")]
    public class TourCreated : IEvent
    {
        public int TourId { get; set; }

        [JsonConstructor]
        public TourCreated(int tourId)
        {
            TourId = tourId;
        }
    }
}