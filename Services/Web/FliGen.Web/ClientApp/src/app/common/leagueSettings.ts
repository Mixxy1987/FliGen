export class LeagueSettings {
  leagueId: number;
  visibility: boolean;
  requireConfirmation: boolean;

  constructor(visibility: boolean, requireConfirmation: boolean, leagueId: number) {
    this.visibility = visibility;
    this.requireConfirmation = requireConfirmation;
    this.leagueId = leagueId;
  }
}
