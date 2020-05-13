import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { League } from "../common/league";
import { LeagueInformation } from "../common/leagueInformation";
import { LeagueSettings } from "../common/leagueSettings";
import { LeaguesInfo } from "../common/leaguesInfo";
import { LeagueType } from "../common/leagueType";
import { PlayersInfo } from "../common/playersInfo";
import { Tour } from "../common/tour";
import { Player } from "../common/player";
import { map } from 'rxjs/internal/operators/map';

@Injectable()
export class DataService {
  private playersUrl: string;
  private leaguesUrl: string;
  private myToursUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
      this.playersUrl = this.baseUrl + "players";
      this.leaguesUrl = this.baseUrl + "leagues";
      this.myToursUrl = this.baseUrl + "mytours";
  }

  getMyTours() {
    return this.http.get<Tour[]>(this.myToursUrl);
  }

  async getMyLeagues() {
    return await this.http.get<League[]>(this.leaguesUrl + "/my").toPromise();
  }

  async joinLeague(id: number) : Promise<string> {

    var result =
      await this.http
      .post(this.leaguesUrl + "/join", id, { observe: 'response' })
      .toPromise();

    return result.headers.get('X-Operation');
  }

  async getLeagues() {
    return await this.http.get<League[]>(this.leaguesUrl).toPromise();
  }

  async getLeaguesInfo() {
    return await this.http.get<LeaguesInfo>(this.leaguesUrl + "/info").toPromise();
  }

  async getLeagueTypes() {
    return await this.http.get<LeagueType[]>(this.leaguesUrl + "/types").toPromise();
  }

  async createLeague(league: League): Promise<string>  {

    var result =
      await this.http
        .post<League>(this.leaguesUrl, league, { observe: 'response' })
        .toPromise();

    return result.headers.get('X-Operation');
  }

  async updateLeague(league: League) : Promise<string> {
    var result =
      await this.http
        .put(this.leaguesUrl + "/update", league, { observe: 'response' })
        .toPromise();

    return result.headers.get('X-Operation');
  }

  async deleteLeague(id: number): Promise<string> {
    var result = await this.http
      .delete(this.leaguesUrl + "/" + id, { observe: 'response' })
      .toPromise();

    return result.headers.get('X-Operation');
  }
  
  async getLeagueInformation(id: number) {
    return await this.http.get<LeagueInformation>(this.leaguesUrl + "/" + id).toPromise();
  }

  async getPlayers() {
    return await this.http.get<Player[]>(this.playersUrl).toPromise();
  }

  async getPlayersInfo() {
    return await this.http.get<PlayersInfo>(this.playersUrl + "/info").toPromise();
  }

  createPlayer(player: Player) {
    return this.http.post<Player>(this.playersUrl, player);
  }

  updatePlayer(player: Player) {
    return this.http.put(this.playersUrl, player);
  }

  deletePlayer(id: number) {
    return this.http.delete(this.playersUrl + "/" + id);
  }

  async getLeagueSettings(id: number) {
    return await this.http.get<LeagueSettings>(this.leaguesUrl + "/" + id + "/settings").toPromise();;
  }

  updateLeagueSettings(leagueSettings: LeagueSettings) {
    return this.http.put(this.leaguesUrl + "/updateSettings", leagueSettings);
  }
}
