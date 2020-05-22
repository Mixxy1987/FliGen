export class PlayerRegisterOnTour {
  private readonly leagueId: number;
  private readonly tourId: number;

  public constructor(tourId: number, leagueId: number) {
    this.leagueId = leagueId;
    this.tourId = tourId;
  }
}
