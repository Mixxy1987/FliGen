import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatSlideToggleModule } from '@angular/material';
import { FocusMonitor } from '@angular/cdk/a11y';
import { Platform } from '@angular/cdk/platform';
import { ContentObserver } from '@angular/cdk/observers';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { PlayersComponent } from "./players/players.component";
import { LeaguesComponent } from "./leagues/leagues.component";
import { ApiAuthorizationModule } from "./api-authorization/api-authorization.module";
import { AuthorizeInterceptor } from "./api-authorization/authorize.interceptor";
import { AppRoutingModule } from "./app-routing.module";
import { DataComponent } from "./data/data.component";
import { InternalDataComponent } from "./internal-data/internal-data.component";
import { ExchangeRateComponent } from "./exchange-rate/exchange-rate.component";
import { MyLeaguesComponent } from "./myleagues/myleagues.component";
import { LeagueDetailComponent } from "./league-detail/league-detail.component";
import { LeagueSettingsComponent } from "./league-detail/league-settings/league-settings.component";
import { MyToursComponent } from "./mytours/mytours.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PlayersComponent,
    LeaguesComponent,
    MyLeaguesComponent,
    DataComponent,
    ExchangeRateComponent,
    InternalDataComponent,
    LeagueDetailComponent,
    LeagueSettingsComponent,
    MyToursComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ApiAuthorizationModule,
    FormsModule,
    AppRoutingModule,
    MatSlideToggleModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    FocusMonitor,
    Platform,
    ContentObserver],
  bootstrap: [AppComponent]
})
export class AppModule { }