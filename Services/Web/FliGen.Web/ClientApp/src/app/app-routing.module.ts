import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { AuthorizeGuard } from "./api-authorization/authorize.guard";
import { PlayersComponent } from "./players/players.component";
import { LeaguesComponent } from "./leagues/leagues.component";
import { MyLeaguesComponent } from "./myleagues/myleagues.component";
import { LeagueDetailComponent } from "./league-detail/league-detail.component";
import { LeagueSettingsComponent } from "./league-detail/league-settings/league-settings.component";
import { MyToursComponent } from "./mytours/mytours.component";


const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'players', component: PlayersComponent, canActivate: [AuthorizeGuard] },
  { path: 'mytours', component: MyToursComponent, canActivate: [AuthorizeGuard]},
  { path: 'leagues', component: LeaguesComponent },
  { path: 'leagues/:id', component: LeagueDetailComponent },
  { path: 'leagues/:id/settings', component: LeagueSettingsComponent },
  { path: 'myleagues', component: MyLeaguesComponent, canActivate: [AuthorizeGuard] },
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
