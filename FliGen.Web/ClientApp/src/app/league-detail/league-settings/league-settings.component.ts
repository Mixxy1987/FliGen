import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { LeagueDataService } from "../league-data.service";
import { LeagueSettings } from "../../common/leagueSettings";
import { AuthorizeService } from "../../api-authorization/authorize.service";

@Component({
  selector: 'app-league-settings',
  templateUrl: './league-settings.component.html',
  providers: [LeagueDataService]
})
export class LeagueSettingsComponent implements OnInit {
  isAuthenticated: boolean;
  id: number;
  leagueSettings: LeagueSettings;
  loaded: boolean = false;

  constructor(
    private dataService: LeagueDataService,
    activeRoute: ActivatedRoute,
    private authorizeService: AuthorizeService)
  {
    this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
  }

  async ngOnInit() {
    debugger;
    this.isAuthenticated = await this.authorizeService.isAuthenticated().pipe(
      take(1)
    ).toPromise();

    if (this.id)
      this.dataService.getLeagueSettings(this.id)
        .subscribe((data: LeagueSettings) => { this.leagueSettings = data; this.loaded = true; });
  }
}
