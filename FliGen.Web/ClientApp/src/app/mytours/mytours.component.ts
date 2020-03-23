import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { MyTour } from "../common/myTour";
import { DataService } from "../data-service/data.service";

@Component({
  selector: 'app-mytours',
  templateUrl: './mytours.component.html',
  providers: [DataService]
})
export class MyToursComponent implements OnInit {
  private myTours: MyTour[];

  isAuthenticated: boolean;

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

  loadMyTours() {
    this.dataService.getMyTours().subscribe(result => {
      this.myTours = result;
    }, error => console.error(error));
  }
}
