import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { Tour } from "../common/tour";
import { DataService } from "../data-service/data.service";
import { ToursQueryType } from "../common/toursQueryType";

@Component({
  selector: 'app-mytours',
  templateUrl: './mytours.component.html',
  providers: [DataService]
})
export class MyToursComponent implements OnInit {
  private incomingTours: Tour[];
  private allTours: Tour[];
  private isAuthenticated: boolean;
  private loaded = false;

  constructor(
    private dataService: DataService,
    private authorizeService: AuthorizeService) {
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
    this.loaded = true;
  }
}
