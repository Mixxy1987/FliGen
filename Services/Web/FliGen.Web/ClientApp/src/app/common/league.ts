import { LeagueType } from "./leagueType";
import { PlayerLeagueJoinStatus } from "./playerLeagueJoinStatus";

export class League {
  id: number;
  name: string;
  description: string;
  leagueType: LeagueType;
  joinStatus: PlayerLeagueJoinStatus;
}
