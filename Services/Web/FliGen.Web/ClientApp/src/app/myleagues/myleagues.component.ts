import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { League } from "../common/league";
import { LeagueType } from "../common/leagueType";
import { DataService } from "../data-service/data.service";
import { SignalRService } from "../services/signalR.service";
import { PageEvent } from "@angular/material/paginator/paginator";
import { Consts } from "../common/consts/consts";

@Component({
  selector: 'app-leagues',
  templateUrl: './myleagues.component.html',
  providers: [DataService]
})
export class MyLeaguesComponent implements OnInit {
  private pageEvent: PageEvent;
  private readonly pageSizeOptions = [3, 5, 10, 25, 100];
  private pageIndex = Consts.leaguesDefaultPageIndex;
  private pageSize = Consts.leaguesDefaultPageSize;

  private leagues: PagedResult<League>;
  private leagueTypes: LeagueType[];
  private loaded: boolean = false;
  private isAuthenticated: boolean;
  private leaguesCount: number;

  constructor(
    private readonly dataService: DataService,
    private readonly authorizeService: AuthorizeService,
    private readonly signalrService: SignalRService) {
  }

  async ngOnInit() {
    this.isAuthenticated = await this.authorizeService.isAuthenticated().pipe(
      take(1)
    ).toPromise();

    var accessToken = await this.authorizeService.getAccessToken().pipe(
      take(1)
    ).toPromise();

    await this.loadLeagues();
    await this.loadLeagueTypes();
    this.loaded = true;
  }

  async loadLeagues(event?: PageEvent) {
    if (event) {
      this.leagues = await this.dataService.getMyLeagues(event.pageSize, event.pageIndex);
      this.pageIndex = event.pageIndex;
      this.pageSize = event.pageSize;
    } else {
      this.leagues = await this.dataService.getMyLeagues();
    }

    this.leaguesCount = this.leagues.totalResults;
    return event;
  }

  async loadLeagueTypes() {
    this.leagueTypes = await this.dataService.getLeagueTypes();
  }

  async joinLeague(l: League) {
    var requestId = await this.dataService.joinLeague(l.id);
    this.signalrService.registerCallback(requestId, () => this.loadLeagues());
  }
}
