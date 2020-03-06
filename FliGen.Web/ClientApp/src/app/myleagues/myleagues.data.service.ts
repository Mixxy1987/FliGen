import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { League } from "../common/league";
import { LeagueType } from "../common/leagueType";

@Injectable()
export class MyLeaguesDataService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getLeagues() {
    return this.http.get<League[]>(this.baseUrl + 'myleagues');
  }

  getLeagueTypes() {
    return this.http.get<LeagueType[]>(this.baseUrl + 'leagues/types');
  }
}
