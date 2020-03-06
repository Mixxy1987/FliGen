import { Component, Inject, OnInit } from '@angular/core';
import { League } from "../common/league";
import { LeagueType } from "../common/leagueType";
import { MyLeaguesDataService } from "./myleagues.data.service";

@Component({
  selector: 'app-leagues',
  templateUrl: './myleagues.component.html',
  providers: [MyLeaguesDataService]
})
export class MyLeaguesComponent implements OnInit {

  private leagues: League[];
  private leagueTypes: LeagueType[];

  constructor(
    private dataService: MyLeaguesDataService) {
  }

  ngOnInit(): void {
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
}
