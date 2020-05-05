import { FocusMonitor } from '@angular/cdk/a11y';
import { ContentObserver } from '@angular/cdk/observers';
import { Platform } from '@angular/cdk/platform';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule, MatInputModule, MatSlideToggleModule, MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material';
import { BrowserModule } from '@angular/platform-browser';
import { ApiAuthorizationModule } from "./api-authorization/api-authorization.module";
import { AuthorizeInterceptor } from "./api-authorization/authorize.interceptor";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LeagueDetailComponent } from "./league-detail/league-detail.component";
import { LeagueSettingsComponent } from "./league-detail/league-settings/league-settings.component";
import { LeaguesComponent } from "./leagues/leagues.component";
import { MyLeaguesComponent } from "./myleagues/myleagues.component";
import { MyToursComponent } from "./mytours/mytours.component";
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { PlayersComponent } from "./players/players.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PlayersComponent,
    LeaguesComponent,
    MyLeaguesComponent,
    LeagueDetailComponent,
    LeagueSettingsComponent,
    MyToursComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ApiAuthorizationModule,
    FormsModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MatSlideToggleModule,
    MatInputModule,
    MatFormFieldModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
    FocusMonitor,
    Platform,
    ContentObserver],
  bootstrap: [AppComponent]
})
export class AppModule { }
