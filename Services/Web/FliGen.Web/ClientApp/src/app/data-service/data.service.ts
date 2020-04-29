import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { League } from "../common/league";
import { LeagueInformation } from "../common/leagueInformation";
import { LeagueSettings } from "../common/leagueSettings";
import { LeaguesInfo } from "../common/leaguesInfo";
import { LeagueType } from "../common/leagueType";
import { PlayersInfo } from "../common/playersInfo";
import { Tour } from "../common/tour";
import { Player } from '../players/player';

@Injectable()
export class DataService {
  private playersUrl: string;
  private leaguesUrl: string;
  private leagueUrl: string;
  private myToursUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
      this.playersUrl = this.baseUrl + "players";
      this.leaguesUrl = this.baseUrl + "leagues";
      this.leagueUrl = this.baseUrl + "league";
      this.myToursUrl = this.baseUrl + "mytours";
  }

  getMyTours() {
    return this.http.get<Tour[]>(this.myToursUrl);
  }

  async getMyLeagues() {
    return await this.http.get<League[]>(this.leaguesUrl + "/my").toPromise();
  }

  joinLeague(id: number) {
    return this.http.post(this.leaguesUrl + "/join", id);
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

  createLeague(league: League) {
    return this.http.post<League>(this.leaguesUrl, league);
  }

  updateLeague(league: League) {
    debugger;
    return this.http.put(this.leagueUrl, league);
  }

  deleteLeague(id: number) {
    return this.http.delete(this.leaguesUrl + "/" + id);
  }
  
  getLeagueInformation(id: number) {
    return this.http.get<LeagueInformation>(this.leagueUrl + "/" + id);
  }

  getPlayers() {
    return this.http.get<Player[]>(this.playersUrl);
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

  getLeagueSettings(id: number) {
    return this.http.get<LeagueSettings>(this.leagueUrl + "/settings/" + id);
  }

  updateLeagueSettings(leagueSettings: LeagueSettings) {
    debugger;
    return this.http.put(this.leagueUrl + "/changeSettings", leagueSettings);
  }
}
