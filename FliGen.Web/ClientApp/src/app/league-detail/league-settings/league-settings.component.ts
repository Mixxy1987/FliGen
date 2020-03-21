import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../../api-authorization/authorize.service";
import { LeagueSettings } from "../../common/leagueSettings";
import { DataService } from "../../data-service/data.service";

@Component({
  selector: 'app-league-settings',
  templateUrl: './league-settings.component.html',
  providers: [DataService]
})
export class LeagueSettingsComponent implements OnInit {
  isAuthenticated: boolean;
  id: number;
  leagueSettings: LeagueSettings;
  loaded: boolean = false;

  constructor(
    private dataService: DataService,
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
