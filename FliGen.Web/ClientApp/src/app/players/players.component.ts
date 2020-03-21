import { Component, OnInit } from '@angular/core';
import { DataService } from "../data-service/data.service";
import { Player } from "./player";

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  providers: [DataService]
})
export class PlayersComponent implements OnInit {

  private player: Player = new Player();
  private players: Player[];
  tableMode: boolean = true;

  constructor(
    private dataService: DataService) { }

  ngOnInit(): void {
    this.loadPlayers();
  }

  loadPlayers() {
    this.dataService.getPlayers().subscribe(result => {
      this.players = result;
    }, error => console.error(error));
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
