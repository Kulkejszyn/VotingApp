import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SignalrService } from '../signalr.service';
import { Vote } from '../../models/votes/vote';
import { AddVoteRequest } from './addVoteRequest';

@Injectable({
  providedIn: 'root'
})
export class VotesService {

  constructor(private http: HttpClient,
              private signalRService: SignalrService) {
  }

  getAllVotes$(): Observable<Vote[]> {
    return this.http.get<Vote[]>('Votes/GetAllVotes');
  }

  addVote$(vote: AddVoteRequest): Observable<void> {
    return this.http.post<void>('Votes/AddVote', vote);
  }

  onVoteAdded$(): Observable<Vote> {
    return this.signalRService.signalRObservable<Vote>('VoteHub', 'OnVoteAdd');
  }
}
