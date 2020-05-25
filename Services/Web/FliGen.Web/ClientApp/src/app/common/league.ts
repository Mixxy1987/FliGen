import { LeagueType } from "./leagueType";
import { PlayerWithLeagueStatus } from "./playerWithLeagueStatus";

export class League{
  id: number;
  name: string;
  description: string;
  leagueType: LeagueType;
  playersLeagueStatuses: PlayerWithLeagueStatus[];
}
