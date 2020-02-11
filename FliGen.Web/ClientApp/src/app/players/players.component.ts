import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-players',
  templateUrl: './players.component.html'
})
export class PlayersComponent {
  public player: Player = new Player();
  public players: Player[];
  http: HttpClient;
  baseUrl: string;
  tableMode: boolean = false;          // табличный режим


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
      this.baseUrl = baseUrl;
    http.get<Player[]>(baseUrl + 'players').subscribe(result => {
      this.players = result;
    }, error => console.error(error));
  }

  editProduct(p: Player) {
    this.player = p;
    }

  save() {
    this.http.post<Player>(this.baseUrl + 'players/add', this.player).subscribe((data: Player) => this.players.push(data));
  }

  cancel() {
      this.player = new Player();
      this.tableMode = true;
    }
  delete(p: Player) {
   
  }
  add() {
    this.cancel();
    this.tableMode = false;
  }
}

export class Player {
  firstName: string;
  lastName: string;
  rate: string;
}
