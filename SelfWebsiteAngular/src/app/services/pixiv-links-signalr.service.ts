import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr"
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PixivLinksSignalrService {
  private pixivLinksHubUrl: string = environment.pixivLinksHubUrl;
  private hubConnection: HubConnection;
  private $allFeed: Subject<string> = new Subject<string>();

  constructor() { }

  public startConnection() {
    return new Promise((resolve, reject) => {
      this.hubConnection = new HubConnectionBuilder()
        .withUrl(this.pixivLinksHubUrl)
        .build();

      this.hubConnection.start()
        .then(() => {
          return resolve(true);
        })
        .catch((err: any) => {
          reject(err);
        });
    });
  }

  public get AllFeedObservable(): Observable<string> {
    return this.$allFeed.asObservable();
  }

  public listen() {
    (this.hubConnection).on("GetPixivLinks", (data: string) => {
      this.$allFeed.next(data);
    });
  }
}
