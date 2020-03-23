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

  tableMode: boolean = true;
  isAuthenticated: boolean;

  constructor(
    private dataService: DataService,
    private authorizeService: AuthorizeService) {
  }

  async ngOnInit() {
    this.isAuthenticated = await this.authorizeService.isAuthenticated().pipe(
      take(1)
    ).toPromise();
    this.loadLeagues();
    this.loadLeagueTypes();
  }

  loadLeagues() {
    this.dataService.getLeagues().subscribe(result => {
        this.leagues = result;
      },
      error => console.error(error));
  }

  loadLeagueTypes() {
    this.dataService.getLeagueTypes().subscribe(result => {
        this.leagueTypes = result;
      },
      error => console.error(error));
  }

  editLeague(l: League) {
    this.newLeagueType = new LeagueType(this.leagueTypes[0].name);
    this.league = l;
  }

  save() {
    this.league.leagueType = new LeagueType(this.newLeagueType.name);
    if (this.league.id == null) {
      this.dataService.createLeague(this.league)
        .subscribe((data: League) => {
          this.leagues.push(data);
          this.loadLeagues();
        });
    } else {
      this.dataService.updateLeague(this.league)
        .subscribe(data => this.loadLeagues());
    }
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
