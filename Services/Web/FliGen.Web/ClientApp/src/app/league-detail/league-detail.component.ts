import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { LeagueInformation } from "../common/leagueInformation";
import { DataService } from "../data-service/data.service";

@Component({
  selector: 'app-league-detail',
  templateUrl: './league-detail.component.html',
  providers: [DataService]
})
export class LeagueDetailComponent implements OnInit {
  private isAuthenticated: boolean;
  private readonly id: number;
  private leagueInformation: LeagueInformation;
  private loaded: boolean = false;

  constructor(
    private dataService: DataService,
    activeRoute: ActivatedRoute,
    private authorizeService: AuthorizeService)
  {
    this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
  }

  async ngOnInit() {
    this.isAuthenticated = await this.authorizeService.isAuthenticated().pipe(
      take(1)
    ).toPromise();

    if (this.id) {
      this.leagueInformation = await this.dataService.getLeagueInformation(this.id);
      this.loaded = true;
    }
  }
}
