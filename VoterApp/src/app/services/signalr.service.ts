import { Inject, Injectable } from '@angular/core';
import { HubConnectionBuilder } from '@microsoft/signalr'
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {


  constructor(@Inject('BASE_API_URL') private baseUrl: string) {
  }

  signalRObservable<T>(hub: string, method: string): Observable<T> {
    const hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/${hub}`)
      .build();

    const subject: Subject<T> = new Subject<T>();

    hubConnection.on(method, (value: T) => {
      subject.next(value);
    });

    hubConnection.onclose((err?: Error) => {
      if (err) {
        subject.error(err);
      } else {
        subject.complete();
      }
    });

    hubConnection.start();
    return subject.asObservable();
  }

}
