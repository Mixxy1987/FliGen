import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { League } from "../common/league";
import { LeagueType } from "../common/leagueType";
import { DataService } from "../data-service/data.service";
import { SignalRService } from "../services/signalR.service";

@Component({
  selector: 'app-leagues',
  templateUrl: './myleagues.component.html',
  providers: [DataService]
})
export class MyLeaguesComponent implements OnInit {

  private leagues: League[];
  private leagueTypes: LeagueType[];
  private loaded: boolean = false;
  private isAuthenticated: boolean;

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

  async loadLeagues() {
    this.leagues = await this.dataService.getMyLeagues();
  }

  async loadLeagueTypes() {
    this.leagueTypes = await this.dataService.getLeagueTypes();
  }

  async joinLeague(l: League) {
    var requestId = await this.dataService.joinLeague(l.id);
    this.signalrService.registerCallback(requestId, () => this.loadLeagues());
  }
}
