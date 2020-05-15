import { Inject, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from "../../environments/environment";
import { AuthorizeService } from "../api-authorization/authorize.service";
import { Dictionary } from "../utils/dictionary";
import { ICallbackFunc } from "../utils/iCallbackFunc";
import { IDictionary } from "../utils/iDictionary";

@Injectable({ providedIn: 'root' })
export class SignalRService {

  private connectionEstablished$ = new BehaviorSubject<boolean>(false);
  private connection: HubConnection;
  private accessToken: string;

  private callbacks: IDictionary<ICallbackFunc>;

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private readonly authorizeService: AuthorizeService) {

    this.callbacks = new Dictionary<ICallbackFunc>();

    this.authorizeService.getAccessToken().pipe(
      take(1)
    ).subscribe(data => this.accessToken = data);

    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  public registerCallback(key: string, value: ICallbackFunc) {
    this.callbacks.add(key, value);
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
      console.log("Operation pending.");
    });

    this.connection.on('operation_completed', (operation) => {
      var id = operation.id;

      if (id) {
        var key = `operations/${id}`;
        if (this.callbacks.containsKey(key)) {
          this.callbacks[key]();
        }
      }

      console.log("Operation completed.");
    });

    this.connection.on('operation_rejected', (operation) => {
      console.log("Operation rejected.");
    });
  }
}
