import { LeagueType } from "./leagueType";
import { JoinStatus } from "./joinStatus";

export class League {
  id: number;
  name: string;
  description: string;
  leagueType: LeagueType;
  joinStatus: JoinStatus;
}
