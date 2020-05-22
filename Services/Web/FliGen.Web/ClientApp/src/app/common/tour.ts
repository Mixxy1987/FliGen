import { TourStatus } from "./tourStatus";

export class Tour {
  id: number;
  date: string;
  homeCount: number;
  guestCount: number;
  leagueId: number;
  seasonId: number;
  tourStatus: TourStatus;
  playerRegistered: boolean;
}
