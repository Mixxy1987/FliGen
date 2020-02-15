import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { League } from "./league";
import { LeaguesDataService } from "./leagues.data.service";
import { LeagueType } from "./leagueType";

@Component({
  selector: 'app-leagues',
  templateUrl: './leagues.component.html',
  providers: [LeaguesDataService]
})
export class LeaguesComponent implements OnInit {

  private league: League = new League();
  private leagues: League[];
  private leagueTypes: LeagueType[];
  tableMode: boolean = true;

  constructor(
    private dataService: LeaguesDataService) { }

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

  /*editProduct(p: Player) {
    this.player = p;
  }*/

  save() {
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

  delete(p: League) {
    this.dataService.delete(p.id)
      .subscribe(data => this.loadLeagues());
  }

  add() {
    this.cancel();
    this.tableMode = false;
  }
}
