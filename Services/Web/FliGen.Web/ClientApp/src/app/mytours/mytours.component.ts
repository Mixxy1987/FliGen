import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { Consts } from "../common/consts/consts";
import { Tour } from "../common/tour";
import { ToursQueryType } from "../common/toursQueryType";
import { DataService } from "../data-service/data.service";
import { SignalRService } from "../services/signalR.service";

@Component({
  selector: 'app-mytours',
  templateUrl: './mytours.component.html',
  providers: [DataService]
})
export class MyToursComponent implements OnInit {
  private leagueIdNameMap: string[] = [];

  private pageEvent: PageEvent;
  private readonly pageSizeOptions = [3, 5, 10, 25, 100];

  private incomingTours: PagedResult<Tour>;
  private allTours: PagedResult<Tour>;
  private isAuthenticated: boolean;
  private loaded = false;

  private incomingToursCount: number;
  private allToursCount: number;
  private incomingToursPageIndex = Consts.toursDefaultPageIndex;
  private incomingToursPageSize = Consts.toursDefaultPageSize;
  private allToursPageIndex = Consts.toursDefaultPageIndex;
  private allToursPageSize = Consts.toursDefaultPageSize;

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

  async loadMyTours(event?: PageEvent, type?: ToursQueryType) {
    if (type === undefined) {
      this.incomingTours = await this.dataService.getToursForPlayer(ToursQueryType.Incoming, this.incomingToursPageSize, this.incomingToursPageIndex);
      this.allTours = await this.dataService.getToursForPlayer(ToursQueryType.All, this.allToursPageSize, this.allToursPageIndex);
    } else {
      if (type === ToursQueryType.Incoming) {
        this.incomingTours = await this.dataService.getToursForPlayer(ToursQueryType.Incoming, event.pageSize, event.pageIndex);
        this.incomingToursPageIndex = event.pageIndex;
        this.incomingToursPageSize = event.pageSize;
      } else {
        this.allTours = await this.dataService.getToursForPlayer(ToursQueryType.All, event.pageSize, event.pageIndex);
        this.allToursPageIndex = event.pageIndex;
        this.allToursPageSize = event.pageSize;
      }
    }

    this.incomingToursCount = this.incomingTours.totalResults;
    this.allToursCount = this.allTours.totalResults;

    const leaguesId = [...new Set(
      this.allTours.items.map(t => t.leagueId)
        .concat(this.incomingTours.items.map(t => t.leagueId)))];

    const leaguesInfo = (await this.dataService.getLeagues(leaguesId)).items;
    leaguesInfo.forEach(l => this.leagueIdNameMap[l.id] = l.name);

    this.loaded = true;
    return event;
  }

  async registerOnTour(t: Tour) {
    const requestId = await this.dataService.registerOnTour(t.id, t.leagueId);
    this.signalrService.registerCallback(requestId, () => this.loadMyTours());
  }
}
