import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { League } from "../common/league";
import { LeagueType } from "../common/leagueType";

@Injectable()
export class LeaguesDataService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getLeagues() {
    return this.http.get<League[]>(this.baseUrl + 'leagues');
  }

  getLeagueTypes() {
    return this.http.get<LeagueType[]>(this.baseUrl + 'leagues/types');
  }

  create(league: League) {
    return this.http.post<League>(this.baseUrl + 'leagues', league);
  }

  update(league: League) {
    return this.http.put(this.baseUrl + 'leagues', league);
  }

  delete(id: number) {
      return this.http.delete(this.baseUrl + 'leagues/' + id);
  }

  join(id: number) {
    return this.http.post(this.baseUrl + 'myleagues/join', id);
  }
}
