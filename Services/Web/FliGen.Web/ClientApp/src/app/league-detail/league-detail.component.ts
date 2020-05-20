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
  private loaded: boolean = false;

  constructor(
    private dataService: DataService,
    activeRoute: ActivatedRoute,
    private authorizeService: AuthorizeService)
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
      const allTours = (await this.dataService.getLeaguesSeasonsId(this.id, LeaguesSeasonsIdQueryType.All)).map(t => t.seasonId);
      debugger;
      if (actualSeason.length > 0) {
        this.allTours = await this.dataService.getTours(0, allTours, ToursQueryType.All);
        this.incomingTours = await this.dataService.getTours(0, actualSeason, ToursQueryType.Incoming);
      }

      this.loaded = true;
    }
  }
}
