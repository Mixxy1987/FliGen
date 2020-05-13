import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { League } from "../common/league";
import { LeagueType } from "../common/leagueType";
import { DataService } from "../data-service/data.service";
import { SignalRService } from "../services/signalR.service";

@Component({
  selector: 'app-leagues',
  templateUrl: './leagues.component.html',
  providers: [DataService]
})
export class LeaguesComponent implements OnInit {
  private league: League = new League();
  private leagues: League[];
  private leagueTypes: LeagueType[];
  private newLeagueType: LeagueType;

  private tableMode: boolean = true;
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
    await this.loadLeagues();
    await this.loadLeagueTypes();
  }

  async loadLeagues() {
    this.leagues = await this.dataService.getLeagues();
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
    let requestId = await this.dataService.deleteLeague(l.id);
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
