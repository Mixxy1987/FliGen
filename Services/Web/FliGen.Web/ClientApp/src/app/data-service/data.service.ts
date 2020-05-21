import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { League } from "../common/league";
import { LeagueInformation } from "../common/leagueInformation";
import { LeagueSeasonId } from "../common/LeagueSeasonId";
import { LeagueSettings } from "../common/leagueSettings";
import { LeaguesInfo } from "../common/leaguesInfo";
import { LeaguesSeasonsIdQueryType } from "../common/leaguesSeasonsIdQueryType";
import { LeagueType } from "../common/leagueType";
import { Player } from "../common/player";
import { PlayersInfo } from "../common/playersInfo";
import { Tour } from "../common/tour";
import { ToursQueryType } from "../common/toursQueryType";

@Injectable()
export class DataService {
  private readonly playersUrl: string;
  private readonly leaguesUrl: string;
  private readonly seasonsUrl: string;
  private readonly toursUrl: string;

  constructor(
    private readonly http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
      this.playersUrl = this.baseUrl + "players";
      this.leaguesUrl = this.baseUrl + "leagues";
      this.seasonsUrl = this.baseUrl + "seasons";
      this.toursUrl = this.baseUrl + "tours";
  }

  async getToursBySeasons(
    playerId: number,
    seasonsId: number[],
    queryType: ToursQueryType): Promise<Tour[]> {
      let url = this.toursUrl + "/player/" + playerId + "/seasons?";
      for (let i = 0; i < seasonsId.length; i++) {
        url += `id=${seasonsId[i]}`;
        if (i !== seasonsId.length - 1) {
          url += "&";
        }
      }
      url += `&queryType=${queryType}`;
      return await this.http.get<Tour[]>(url).toPromise();
  }

  async getToursForPlayer(queryType: ToursQueryType): Promise<Tour[]> {
      let url: string = this.toursUrl + "/player";
      url += `?queryType=${queryType}`;
      return await this.http.get<Tour[]>(url).toPromise();
  }

  async getLeaguesSeasonsId(
    id: number,
    queryType: LeaguesSeasonsIdQueryType): Promise<LeagueSeasonId[]> {
    return await this.http.get<LeagueSeasonId[]>(
      this.seasonsUrl + "/leaguesSeasonsId?leagueId=" + id + "&queryType=" + queryType)
      .toPromise();
  }

  async getMyLeagues() {
    return await this.http.get<League[]>(this.leaguesUrl + "/my").toPromise();
  }

  async getLeagues(leaguesId: number[] = null) {
    let url = this.leaguesUrl;
    if (leaguesId !== null) {
      url += "?";
      for (let i = 0; i < leaguesId.length; i++) {
        url += `id=${leaguesId[i]}`;
        if (i !== leaguesId.length - 1) {
          url += "&";
        }
      }
    }
    return await this.http.get<League[]>(url).toPromise();
  }

  async joinLeague(id: number): Promise<string> {
    const result =
      await this.http
        .post(this.leaguesUrl + "/join", id, { observe: 'response' })
        .toPromise();

    return result.headers.get('X-Operation');
  }

  async getLeaguesInfo() {
    return await this.http.get<LeaguesInfo>(this.leaguesUrl + "/info").toPromise();
  }

  async getLeagueTypes() {
    return await this.http.get<LeagueType[]>(this.leaguesUrl + "/types").toPromise();
  }

  async createLeague(league: League): Promise<string> {
    const result =
      await this.http
        .post<League>(this.leaguesUrl, league, { observe: 'response' })
        .toPromise();

    return result.headers.get('X-Operation');
  }

  async updateLeague(league: League): Promise<string> {
    const result =
      await this.http
        .put(this.leaguesUrl + "/update", league, { observe: 'response' })
        .toPromise();

    return result.headers.get('X-Operation');
  }

  async deleteLeague(id: number): Promise<string> {
    const result = await this.http
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
    return await this.http.get<LeagueSettings>(this.leaguesUrl + "/" + id + "/settings").toPromise();
  }

  async updateLeagueSettings(leagueSettings: LeagueSettings) {
    const result = await this.http
      .put(this.leaguesUrl + "/updateSettings", leagueSettings, { observe: 'response' })
      .toPromise();

    return result.headers.get('X-Operation');
  }
}
