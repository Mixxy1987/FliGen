import { LeagueType } from "./leagueType";
import { Season } from "./season";

export class LeagueInformation {
  name: string;
  description: string;
  leagueType: LeagueType;
  seasonsCount: number;
  toursCount: number;
  currentSeason: Season;
}
