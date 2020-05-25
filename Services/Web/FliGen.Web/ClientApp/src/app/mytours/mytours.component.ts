import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { Tour } from "../common/tour";
import { DataService } from "../data-service/data.service";
import { ToursQueryType } from "../common/toursQueryType";
import { Dictionary } from "../utils/dictionary";
import { SignalRService } from "../services/signalR.service";

@Component({
  selector: 'app-mytours',
  templateUrl: './mytours.component.html',
  providers: [DataService]
})
export class MyToursComponent implements OnInit {
  private leagueIdNameMap: string[] = [];

  private incomingTours: Tour[];
  private allTours: Tour[];
  private isAuthenticated: boolean;
  private loaded = false;

  constructor(
    private readonly dataService: DataService,
    private readonly authorizeService: AuthorizeService,
    private readonly signalrService: SignalRService) {
  }

  async ngOnInit() {
    this.isAuthenticated = await this.authorizeService.isAuthenticated().pipe(
      take(1)
    ).toPromise();

    this.loadMyTours();
  }

  async loadMyTours() {
    this.incomingTours = await this.dataService.getToursForPlayer(ToursQueryType.Incoming);
    this.allTours = await this.dataService.getToursForPlayer(ToursQueryType.All);

    const leaguesId = [...new Set(
      this.allTours.map(t => t.leagueId)
        .concat(this.incomingTours.map(t => t.leagueId)))];

    const leaguesInfo = (await this.dataService.getLeagues(leaguesId)).items;
    leaguesInfo.forEach(l => this.leagueIdNameMap[l.id]= l.name);

    this.loaded = true;
  }

  async registerOnTour(t: Tour) {
    const requestId = await this.dataService.registerOnTour(t.id, t.leagueId);
    this.signalrService.registerCallback(requestId, () => this.loadMyTours());
  }
}
