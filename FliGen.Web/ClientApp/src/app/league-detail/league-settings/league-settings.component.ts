import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LeagueSettings } from "../../common/leagueSettings";
import { DataService } from "../../data-service/data.service";

@Component({
  selector: 'app-league-settings',
  templateUrl: './league-settings.component.html',
  providers: [DataService]
})
export class LeagueSettingsComponent implements OnInit {
  id: number;
  leagueSettings: LeagueSettings;
  loaded: boolean = false;

  constructor(
    private dataService: DataService,
    activeRoute: ActivatedRoute)
  {
    this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
  }

  changeVisibility() {
    this.leagueSettings.visibility = !this.leagueSettings.visibility;
    this.updateLeagueSettings(new LeagueSettings(this.leagueSettings.visibility, this.leagueSettings.requireConfirmation, this.id));
  }

  changeConfirmation() {
    this.leagueSettings.requireConfirmation = !this.leagueSettings.requireConfirmation;
    this.updateLeagueSettings(new LeagueSettings(this.leagueSettings.visibility, this.leagueSettings.requireConfirmation, this.id));
  }

  async ngOnInit() {
    this.loadLeagueSettings();
  }

  loadLeagueSettings() {
    if (this.id)
      this.dataService.getLeagueSettings(this.id)
        .subscribe((data: LeagueSettings) => { this.leagueSettings = data; this.loaded = true; });
  }

  updateLeagueSettings(leagueSettings: LeagueSettings) {
    this.dataService.updateLeagueSettings(leagueSettings).subscribe(data => this.loadLeagueSettings());;
  }
}
