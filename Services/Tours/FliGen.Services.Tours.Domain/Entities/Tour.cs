﻿using FliGen.Common.SeedWork;
using FliGen.Common.Types;
using System;
using FliGen.Services.Tours.Domain.Common;
using FliGen.Services.Tours.Domain.Entities.Enum;

namespace FliGen.Services.Tours.Domain.Entities
{
    public class Tour : Entity
    {
        public DateTime Date { get; set; }
        public int? HomeCount { get; set; }
        public int? GuestCount { get; set; }

        public int SeasonId { get; set; }

        public int TourStatusId
        {
            get => TourStatus.Id;
            set => TourStatus = Enumeration.FromValue<TourStatus>(value);
        }

        public TourStatus TourStatus { get; private set; }

        private Tour()
        { }

        private Tour(DateTime date, int seasonId)
        {
            if (seasonId <= 0)
            {
                throw new FliGenException(ErrorCodes.InvalidSeasonId, $"Invalid seasonId. - {seasonId}");
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
            switch (TourStatus.Name)
            { // todo ::refactor?
                case (nameof(TourStatus.Canceled)):
                    {
                        TourStatusId = TourStatus.Planned.Id;
                        break;
                    }
                case (nameof(TourStatus.Planned)):
                    {
                        TourStatusId = TourStatus.RegistrationOpened.Id;
                        break;
                    }
                case (nameof(TourStatus.RegistrationOpened)):
                    {
                        TourStatusId = TourStatus.RegistrationClosed.Id;
                        break;
                    }
                case (nameof(TourStatus.RegistrationClosed)):
                    {
                        TourStatusId = TourStatus.InProgress.Id;
                        break;
                    }
                case (nameof(TourStatus.InProgress)):
                    {
                        TourStatusId = TourStatus.Completed.Id;
                        break;
                    }
            }
        }

        public void MoveTourStatusBack()
        {
            switch (TourStatus.Name)
            {
                case (nameof(TourStatus.RegistrationOpened)):
                {
                    TourStatusId = TourStatus.Planned.Id;
                    break;
                }
                case (nameof(TourStatus.RegistrationClosed)):
                {
                    TourStatusId = TourStatus.RegistrationOpened.Id;
                    break;
                }
                case (nameof(TourStatus.InProgress)):
                {
                    TourStatusId = TourStatus.RegistrationClosed.Id;
                    break;
                }
                case (nameof(TourStatus.Completed)):
                {
                    TourStatusId = TourStatus.InProgress.Id;
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

        public bool IsEnded()
        {
            return TourStatus.Equals(TourStatus.Canceled) || TourStatus.Equals(TourStatus.Completed);
        }

        public bool IsRegistrationOpened()
        {
            return TourStatus.Equals(TourStatus.RegistrationOpened);
        }

        public bool IsRegistrationIsNotYetOpened()
        {
            return TourStatus.Equals(TourStatus.Planned);
        }

        public bool IsRegistrationClosed()
        {
            return TourStatus.Equals(TourStatus.RegistrationClosed) ||
                   TourStatus.Equals(TourStatus.Canceled) ||
                   TourStatus.Equals(TourStatus.Completed) ||
                   TourStatus.Equals(TourStatus.InProgress);
        }
    }
}
