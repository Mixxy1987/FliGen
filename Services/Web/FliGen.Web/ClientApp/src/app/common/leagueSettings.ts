export class LeagueSettings {
  leagueId: number;
  visibility: boolean;
  requireConfirmation: boolean;
  teamsInTour?: number;
  playersInTeam?: number;

  constructor(
    visibility: boolean,
    requireConfirmation: boolean,
    leagueId: number,
    teamsInTour?: number,
    playersInTeam?: number) {
      this.playersInTeam = playersInTeam;
      this.teamsInTour = teamsInTour;
      this.visibility = visibility;
      this.requireConfirmation = requireConfirmation;
      this.leagueId = leagueId;
  }
}
