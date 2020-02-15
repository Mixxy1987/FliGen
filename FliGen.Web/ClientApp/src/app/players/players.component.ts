import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Player } from "./player";
import { PlayersDataService } from "./playersDataService";

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
    public http: HttpClient,
    private dataService: PlayersDataService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this.dataService.get().subscribe(result => {
      this.players = result;
    }, error => console.error(error));
  }

  editProduct(p: Player) {
    this.player = p;
  }

  save() {
    if (this.player.id == null) {
      this.dataService.create(this.player)
          .subscribe((data: Player) => {
              this.players.push(data);
              this.loadProducts();
          });
    } else {
      this.dataService.update(this.player)
        .subscribe(data => this.loadProducts());
      }
    this.cancel();
  }

  cancel() {
    this.player = new Player();
    this.tableMode = true;
  }

  delete(p: Player) {
    this.dataService.delete(p.id)
      .subscribe(data => this.loadProducts());
  }

  add() {
    this.cancel();
    this.tableMode = false;
  }
}
