import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { League } from "../common/league";
import { LeagueType } from "../common/leagueType";
import { DataService } from "../data-service/data.service";

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
    private readonly authorizeService: AuthorizeService) {
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
    if (this.league.id == null) {
      await this.dataService.createLeague(this.league);
    } else {
      await this.dataService.updateLeague(this.league);
    }
    await this.loadLeagues();
    this.cancel();
  }

  cancel() {
    this.league = new League();
    this.tableMode = true;
  }

  delete(l: League) {
    this.dataService.deleteLeague(l.id)
      .subscribe(data => this.loadLeagues());
  }

  add() {
    this.newLeagueType = new LeagueType(this.leagueTypes[0].name);
    this.cancel();
    this.tableMode = false;
  }

  joinLeague(l: League) {
    this.dataService.joinLeague(l.id)
      .subscribe(data => this.loadLeagues());
  }
}
