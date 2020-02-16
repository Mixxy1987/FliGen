import { Component, Inject, OnInit } from '@angular/core';
import { Player } from "./player";
import { PlayersDataService } from "./players.data.service";

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html',
  providers: [PlayersDataService]
})
export class PlayersComponent implements OnInit {

  private player: Player = new Player();
  private players: Player[];
  tableMode: boolean = true;

  constructor(
    private dataService: PlayersDataService) { }

  ngOnInit(): void {
    this.loadPlayers();
  }

  loadPlayers() {
    this.dataService.get().subscribe(result => {
      this.players = result;
    }, error => console.error(error));
  }

  editPlayer(p: Player) {
    this.player = p;
  }

  save() {
    if (this.player.id == null) {
      this.dataService.create(this.player)
        .subscribe((data: Player) => {
          this.players.push(data);
          this.loadPlayers();
        });
    } else {
      this.dataService.update(this.player)
        .subscribe(data => this.loadPlayers());
    }
    this.cancel();
  }

  cancel() {
    this.player = new Player();
    this.tableMode = true;
  }

  delete(p: Player) {
    this.dataService.delete(p.id)
      .subscribe(data => this.loadPlayers());
  }

  add() {
    this.cancel();
    this.tableMode = false;
  }
}
