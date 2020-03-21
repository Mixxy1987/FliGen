import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { take } from 'rxjs/operators';
import { LeagueDataService } from "./league-data.service";
import { ActivatedRoute } from '@angular/router';
import { League } from "../common/league";
import { LeagueInformation } from "../common/leagueInformation";

@Component({
  selector: 'app-league-detail',
  templateUrl: './league-detail.component.html',
  providers: [LeagueDataService]
})
export class LeagueDetailComponent implements OnInit {
  isAuthenticated: boolean;
  id: number;
  leagueInformation: LeagueInformation;
  loaded: boolean = false;

  constructor(
    private dataService: LeagueDataService,
    activeRoute: ActivatedRoute,
    private authorizeService: AuthorizeService)
  {
    this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
  }

  async ngOnInit() {
    this.isAuthenticated = await this.authorizeService.isAuthenticated().pipe(
      take(1)
    ).toPromise();

    if (this.id)
      this.dataService.getLeagueInformation(this.id)
        .subscribe((data: LeagueInformation) => {
          this.leagueInformation = data;
          this.loaded = true;
        });
  }
}
