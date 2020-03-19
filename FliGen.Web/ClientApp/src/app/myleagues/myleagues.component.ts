import { Component, Inject, OnInit } from '@angular/core';
import { League } from "../common/league";
import { LeagueType } from "../common/leagueType";
import { MyLeaguesDataService } from "./myleagues.data.service";
import { AuthorizeService } from "../api-authorization/authorize.service";
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-leagues',
  templateUrl: './myleagues.component.html',
  providers: [MyLeaguesDataService]
})
export class MyLeaguesComponent implements OnInit {

  private leagues: League[];
  private leagueTypes: LeagueType[];

  isAuthenticated: boolean;

  constructor(
    private dataService: MyLeaguesDataService,
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
    }, error => console.error(error));
  }

  loadLeagueTypes() {
    this.dataService.getLeagueTypes().subscribe(result => {
      this.leagueTypes = result;
    }, error => console.error(error));
  }

  joinLeague(l: League) {
    this.dataService.join(l.id)
      .subscribe(data => this.loadLeagues());
  }
}
