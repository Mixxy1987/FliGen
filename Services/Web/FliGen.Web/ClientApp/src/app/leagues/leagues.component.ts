import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { League } from "../common/league";
import { LeagueType } from "../common/leagueType";
import { DataService } from "../data-service/data.service";
import { SignalRService } from "../services/signalR.service";
import { PageEvent } from '@angular/material/paginator';
import { Consts } from "../common/consts/consts";

@Component({
  selector: 'app-leagues',
  templateUrl: './leagues.component.html',
  providers: [DataService]
})
export class LeaguesComponent implements OnInit {

  private pageEvent: PageEvent;
  private readonly pageSizeOptions = [3, 5, 10, 25, 100];
  private pageIndex = Consts.leaguesDefaultPageIndex;
  private pageSize = Consts.leaguesDefaultPageSize;

  private league: League = new League();
  private leagues: PagedResult<League>;
  private leagueTypes: LeagueType[];
  private newLeagueType: LeagueType;

  private tableMode = true;
  private isAuthenticated: boolean;
  private leaguesCount : number;

  constructor(
    private readonly dataService: DataService,
    private readonly authorizeService: AuthorizeService,
    private readonly signalrService: SignalRService) {
  }

  async ngOnInit() {
    this.isAuthenticated = await this.authorizeService.isAuthenticated().pipe(
      take(1)
    ).toPromise();
    await this.loadLeagues();
    await this.loadLeagueTypes();
  }

  async loadLeagues(event?: PageEvent) {
    if (event) {
      this.leagues = await this.dataService.getLeagues(null, event.pageSize, event.pageIndex);
      this.pageIndex = event.pageIndex;
      this.pageSize = event.pageSize;
    } else {
      this.leagues = await this.dataService.getLeagues();
    }
    this.leaguesCount = this.leagues.totalResults;
    return event;
  }

  async loadLeagueTypes() {
    this.leagueTypes = await this.dataService.getLeagueTypes();
  }

  editLeague(l: League) {
    this.newLeagueType = new LeagueType(this.leagueTypes[0].name);
    this.league = l;
  }

  async save() {
    this.league.leagueType = new LeagueType(this.newLeagueType.name);
    let requestId : string;
    if (this.league.id == null) {
      requestId = await this.dataService.createLeague(this.league);
    } else {
      requestId = await this.dataService.updateLeague(this.league);
    }
    this.signalrService.registerCallback(requestId, () => this.loadLeagues());
    this.cancel();
  }

  cancel() {
    this.league = new League();
    this.tableMode = true;
  }

  async delete(l: League) {
    const requestId = await this.dataService.deleteLeague(l.id);
    this.signalrService.registerCallback(requestId, () => this.loadLeagues());
  }

  add() {
    this.newLeagueType = new LeagueType(this.leagueTypes[0].name);
    this.cancel();
    this.tableMode = false;
  }

  async joinLeague(l: League) {
    let requestId = await this.dataService.joinLeague(l.id);
    this.signalrService.registerCallback(requestId, () => this.loadLeagues());
  }
}
