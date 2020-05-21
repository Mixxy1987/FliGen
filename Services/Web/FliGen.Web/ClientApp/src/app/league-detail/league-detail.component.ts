import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { LeagueInformation } from "../common/leagueInformation";
import { LeagueSeasonId } from "../common/LeagueSeasonId";
import { LeaguesSeasonsIdQueryType } from "../common/leaguesSeasonsIdQueryType";
import { Tour } from "../common/tour";
import { ToursQueryType } from "../common/toursQueryType";
import { DataService } from "../data-service/data.service";

@Component({
  selector: 'app-league-detail',
  templateUrl: './league-detail.component.html',
  providers: [DataService]
})
export class LeagueDetailComponent implements OnInit {
  private isAuthenticated: boolean;
  private readonly id: number;
  private leagueInformation: LeagueInformation;
  private leaguesSeasonsId: LeagueSeasonId[];
  private incomingTours: Tour[];
  private allTours: Tour[];
  private loaded = false;

  constructor(
    private readonly dataService: DataService,
    private readonly activeRoute: ActivatedRoute,
    private readonly authorizeService: AuthorizeService)
  {
    this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
  }

  async ngOnInit() {
    this.isAuthenticated = await this.authorizeService.isAuthenticated().pipe(
      take(1)
    ).toPromise();

    if (this.id) {
      this.leagueInformation = await this.dataService.getLeagueInformation(this.id);
      const actualSeason = (await this.dataService.getLeaguesSeasonsId(this.id, LeaguesSeasonsIdQueryType.Actual)).map(t => t.seasonId);
      const allSeasons = (await this.dataService.getLeaguesSeasonsId(this.id, LeaguesSeasonsIdQueryType.All)).map(t => t.seasonId);
      if (actualSeason.length > 0) {
        this.allTours = await this.dataService.getToursBySeasons(0, allSeasons, ToursQueryType.All);
        this.incomingTours = await this.dataService.getToursBySeasons(0, actualSeason, ToursQueryType.Incoming);
      }
      this.loaded = true;
    }
  }
}
