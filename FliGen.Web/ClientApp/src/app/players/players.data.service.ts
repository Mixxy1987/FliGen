import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Player } from "./player";

@Injectable()
export class PlayersDataService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  get() {
    return this.http.get<Player[]>(this.baseUrl + 'players');
  }

  create(player: Player) {
    return this.http.post<Player>(this.baseUrl + 'players', player);
  }

  update(player: Player) {
    return this.http.put(this.baseUrl + 'players', player);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + 'players' + id);
  }
}
