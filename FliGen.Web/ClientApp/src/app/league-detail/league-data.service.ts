import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { LeagueInformation } from "../common/leagueInformation";
import { LeagueSettings } from "../common/leagueSettings";

@Injectable()
export class LeagueDataService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string)
  {}

  getLeagueInformation(id: number) {
    return this.http.get<LeagueInformation>(this.baseUrl + 'league/' + id);
  }

  getLeagueSettings(id: number) {
    return this.http.get<LeagueSettings>(this.baseUrl + 'league/settings/' + id);
  }
}
