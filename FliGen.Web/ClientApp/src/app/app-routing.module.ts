import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { AuthorizeGuard } from "./api-authorization/authorize.guard";
import { AuthorizeWindowsGroupGuardGuard } from "./api-authorization/authorize-windows-group-guard.guard";
import { PlayersComponent } from "./players/players.component";
import { LeaguesComponent } from "./leagues/leagues.component";
import { DataComponent } from "./data/data.component";
import { InternalDataComponent } from "./internal-data/internal-data.component";


const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'data', component: DataComponent, canActivate: [AuthorizeGuard] },
  { path: 'internaldata', component: InternalDataComponent, canActivate: [AuthorizeWindowsGroupGuardGuard] },
  { path: 'players', component: PlayersComponent, canActivate: [AuthorizeGuard]  },
  { path: 'leagues', component: LeaguesComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
