import { Component, Inject, OnInit } from '@angular/core';
import { LeaguesDataService } from "./leagues.data.service";
import { League } from "../common/league";
import { LeagueType } from "../common/leagueType";

@Component({
  selector: 'app-leagues',
  templateUrl: './leagues.component.html',
  providers: [LeaguesDataService]
})
export class LeaguesComponent implements OnInit {

  private league: League = new League();
  private leagues: League[];
  private leagueTypes: LeagueType[];
  private newLeagueType: LeagueType;
  tableMode: boolean = true;

  constructor(
    private dataService: LeaguesDataService) {

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

  editLeague(l: League) {
    this.newLeagueType = new LeagueType(this.leagueTypes[0].name);
    this.league = l;
  }

  save() {
    this.league.leagueType = new LeagueType(this.newLeagueType.name);
    if (this.league.id == null) {
      this.dataService.create(this.league)
        .subscribe((data: League) => {
          this.leagues.push(data);
          this.loadLeagues();
        });
    } else {
      this.dataService.update(this.league)
        .subscribe(data => this.loadLeagues());
    }
    this.cancel();
  }

  cancel() {
    this.league = new League();
    this.tableMode = true;
  }

  delete(l: League) {
    this.dataService.delete(l.id)
      .subscribe(data => this.loadLeagues());
  }

  add() {
    this.newLeagueType = new LeagueType(this.leagueTypes[0].name);
    this.cancel();
    this.tableMode = false;
  }

  joinLeague(l: League) {
    this.dataService.join(l.id)
      .subscribe(data => this.loadLeagues());
  }
}
