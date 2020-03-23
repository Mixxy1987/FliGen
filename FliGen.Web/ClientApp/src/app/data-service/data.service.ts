import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { League } from "../common/league";
import { LeagueInformation } from "../common/leagueInformation";
import { LeagueSettings } from "../common/leagueSettings";
import { LeagueType } from "../common/leagueType";
import { MyTour } from "../common/myTour";
import { Player } from '../players/player';

@Injectable()
export class DataService {
  private playersUrl: string;
  private leaguesUrl: string;
  private leagueUrl: string;
  private myLeaguesUrl: string;
  private myToursUrl: string;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
      this.playersUrl = this.baseUrl + "players";
      this.leaguesUrl = this.baseUrl + "leagues";
      this.leagueUrl = this.baseUrl + "league";
      this.myLeaguesUrl = this.baseUrl + "myleagues";
      this.myToursUrl = this.baseUrl + "mytours";
  }

  getMyTours() {
    return this.http.get<MyTour[]>(this.myToursUrl);
  }

  getMyLeagues() {
    return this.http.get<League[]>(this.myLeaguesUrl);
  }

  joinLeague(id: number) {
    return this.http.post(this.myLeaguesUrl + "/join", id);
  }

  getLeagues() {
    return this.http.get<League[]>(this.leaguesUrl);
  }

  getLeagueTypes() {
    return this.http.get<LeagueType[]>(this.leaguesUrl + "/types");
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
