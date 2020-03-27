﻿using System;
using System.Collections.Generic;
using FliGen.Common.SeedWork;
using FliGen.Common.Types;
using FliGen.Domain.Entities.Enum;

namespace FliGen.Domain.Entities
{
    public class Tour : Entity
    {
        public DateTime Date { get; set; }
        public List<Team> Teams { get; set; }
        public int? HomeCount { get; set; }
        public int? GuestCount { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }

        public int TourStatusId
        {
            get => TourStatus.Id;
            set => TourStatus = Enumeration.FromValue<TourStatus>(value);
        }

        public TourStatus TourStatus { get; private set; }

        private Tour()
        {}

        private Tour(DateTime date, int seasonId)
        {
            if (seasonId <= 0)
            {
                throw new FliGenException("invalid_seasonId", $"Invalid seasonId. - {seasonId}");
            }

            Date = date;
            SeasonId = seasonId;
            TourStatusId = TourStatus.Planned.Id;
        }

        public static Tour Create(DateTime date, int seasonId)
        {
            return new Tour(date, seasonId);
        }
        public static Tour Create(string date, int seasonId)
        {
            return new Tour(DateTime.Parse(date), seasonId);
        }

        public void MoveTourStatusForward()
        {
            switch (TourStatus)
            {
                case (TourStatus.Enum.Canceled):
                {
                    TourStatusId = TourStatus.Planned.Id;
                    break;
                }
                case (TourStatus.Enum.Planned):
                {
                    TourStatusId = TourStatus.RegistrationOpened.Id;
                    break;
                }
                case (TourStatus.Enum.RegistrationOpened):
                {
                    TourStatusId = TourStatus.RegistrationClosed.Id;
                    break;
                }
                case (TourStatus.Enum.RegistrationClosed):
                {
                    TourStatusId = TourStatus.InProgress.Id;
                    break;
                }
                case (TourStatus.Enum.InProgress):
                {
                    TourStatusId = TourStatus.Completed.Id;
                    break;
                }
            }
        }

        public void CancelTour()
        {
            if (TourStatus.Equals(TourStatus.InProgress) ||
                TourStatus.Equals(TourStatus.Completed))
            {
                return;
            }

            TourStatusId = TourStatus.Canceled.Id;
        }

    }
}
