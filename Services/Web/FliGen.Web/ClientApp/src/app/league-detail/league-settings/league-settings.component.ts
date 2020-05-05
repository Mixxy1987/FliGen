import { Component, OnInit, AfterViewInit, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LeagueSettings } from "../../common/leagueSettings";
import { DataService } from "../../data-service/data.service";
import { AutofillMonitor } from '@angular/cdk/text-field';

@Component({
  selector: 'app-league-settings',
  templateUrl: './league-settings.component.html',
  providers: [DataService, AutofillMonitor]
})

export class LeagueSettingsComponent implements OnInit{
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

  updateLeagueSettings(leagueSettings: LeagueSettings) {
    this.dataService.updateLeagueSettings(leagueSettings).subscribe(data => this.loadLeagueSettings());;
  }
}
