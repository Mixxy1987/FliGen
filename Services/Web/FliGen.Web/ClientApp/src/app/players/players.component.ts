import { Component, OnInit } from '@angular/core';
import { DataService } from "../data-service/data.service";
import { Player } from "../common/player";

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  providers: [DataService]
})
export class PlayersComponent implements OnInit {

  private player: Player = new Player();
  private players: Player[];
  private tableMode: boolean = true;
  private loaded: boolean = false;

  constructor(
    private dataService: DataService) { }

  async ngOnInit(){
    await this.loadPlayers();
    this.loaded = true;
  }

  async loadPlayers() {
    this.players = await this.dataService.getPlayers();
  }

  editPlayer(p: Player) {
    this.player = p;
  }

  save() {
    if (this.player.id == null) {
      this.dataService.createPlayer(this.player)
        .subscribe((data: Player) => {
          this.players.push(data);
          this.loadPlayers();
        });
    } else {
      this.dataService.updatePlayer(this.player)
        .subscribe(data => this.loadPlayers());
    }
    this.cancel();
  }

  cancel() {
    this.player = new Player();
    this.tableMode = true;
  }

  delete(p: Player) {
    this.dataService.deletePlayer(p.id)
      .subscribe(data => this.loadPlayers());
  }

  add() {
    this.cancel();
    this.tableMode = false;
  }
}
