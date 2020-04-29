import { Component, OnInit } from '@angular/core';
import { DataService } from "../data-service/data.service";
import { LeaguesInfo } from "../common/leaguesInfo";
import { PlayersInfo } from "../common/playersInfo";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [DataService],
  styles: []
})
export class HomeComponent implements OnInit {
  private leaguesInfo: LeaguesInfo;
  private playersInfo: PlayersInfo;
  private loaded: boolean = false;

  constructor(private readonly dataService: DataService) {}

  async ngOnInit() {
    await this.loadLeagues();
    await this.loadPlayers();
    this.loaded = true;
  }

  async loadLeagues() {
    this.leaguesInfo = await this.dataService.getLeaguesInfo();
  }

  async loadPlayers() {
    this.playersInfo = await this.dataService.getPlayersInfo();
  }
}
