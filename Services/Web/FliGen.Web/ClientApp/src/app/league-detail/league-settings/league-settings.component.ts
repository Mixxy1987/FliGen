import { AutofillMonitor } from '@angular/cdk/text-field';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LeagueSettings } from "../../common/leagueSettings";
import { DataService } from "../../data-service/data.service";
import { SignalRService } from "../../services/signalR.service";

@Component({
  selector: 'app-league-settings',
  templateUrl: './league-settings.component.html',
  providers: [DataService, AutofillMonitor]
})

export class LeagueSettingsComponent implements OnInit{
  private readonly id: number;
  private leagueSettings: LeagueSettings;
  private loaded: boolean = false;

  constructor(
    private readonly dataService: DataService,
    private readonly activeRoute: ActivatedRoute,
    private readonly signalrService: SignalRService)
  {
    this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
  }

  changeVisibility() {
    this.leagueSettings.visibility = !this.leagueSettings.visibility;
  }

  changeConfirmation() {
    this.leagueSettings.requireConfirmation = !this.leagueSettings.requireConfirmation;
  }

  save() {
    this.updateLeagueSettings(this.leagueSettings);
  }

  async ngOnInit() {
    await this.loadLeagueSettings();
  }

  async loadLeagueSettings() {
    if (this.id) {
      this.leagueSettings = await this.dataService.getLeagueSettings(this.id);
      this.leagueSettings.leagueId = this.id;
      this.loaded = true;
    }
  }

  async updateLeagueSettings(leagueSettings: LeagueSettings) {

    var requestId = await this.dataService.updateLeagueSettings(leagueSettings);
    this.signalrService.registerCallback(requestId, () => this.loadLeagueSettings());
  }
}
