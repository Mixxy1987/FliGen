import { Injectable, Inject, OnInit } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel
} from '@microsoft/signalr';
import { BehaviorSubject, Subject } from 'rxjs';
import { AuthorizeService } from "../api-authorization/authorize.service";
import { take } from 'rxjs/operators';
import { environment } from "../../environments/environment";

@Injectable({ providedIn: 'root' })
export class SignalRService {

  connectionEstablished$ = new BehaviorSubject<boolean>(false);
  private connection: HubConnection;
  private accessToken: string;

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private readonly authorizeService: AuthorizeService) {

    this.authorizeService.getAccessToken().pipe(
      take(1)
    ).subscribe(data => this.accessToken = data);

    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  private createConnection() {
    this.connection = new HubConnectionBuilder()
      .withUrl(environment.signalrHub)
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();
  }

  private startConnection() {
    if (this.connection.state === HubConnectionState.Connected) {
      return;
    }
    debugger;
    console.log("Connecting to FliGen Hub...");

    this.connection.start().then(
      () => {
        this.connection.invoke('initializeAsync', this.accessToken);
        console.log("Hub connection started!");
        this.connectionEstablished$.next(true);
      },
      error => console.error(error)
    );
  }
  private registerOnServerEvents(): void {
    this.connection.on('connected', _ => {
      console.log("Connected.");
    });

    this.connection.on('disconnected', _ => {
      console.log("Disconnected");
    });

    this.connection.on('operation_pending', (operation) => {
      debugger;
      console.log("Operation pending.");
    });

    this.connection.on('operation_completed', (operation) => {
      debugger;
      console.log("Operation completed.");
    });

    this.connection.on('operation_rejected', (operation) => {
      debugger;
      console.log("Operation rejected.");
    });
  }
}
