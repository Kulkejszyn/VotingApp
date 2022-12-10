import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { VotesService } from '../../../services/votes/votes.service';
import { CandidateViewModel } from './candidate-view.model';
import { VoterViewModel } from './voter-view.model';
import { UsersService } from '../../../services/users/users.service';
import { forkJoin, tap } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'app-voting-results',
  templateUrl: './voting-results.component.html',
  styleUrls: ['./voting-results.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VotingResultsComponent implements OnInit {
  public voters: VoterViewModel[] = [];
  public candidates: CandidateViewModel[] = [];
  public readonly votersDisplayedColumns = ['name', 'surname', 'hasVoted'];
  public readonly candidatesDisplayedColumns = ['name', 'surname', 'votes'];

  constructor(private votingService: VotesService,
              private userService: UsersService,
              private cdr: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.fetchVotes();
    this.observeToVotingChanges();
  }

  private observeToVotingChanges() {
    this.votingService.onVoteAdded$().pipe(
      untilDestroyed(this),
      tap(vote => {
        const voter = this.voters.find(voter => voter.user.id === vote.votingUser.id)
        if (voter) {
          voter.hasVoted = true;
        } else {
          this.voters = [...this.voters, {
            user: vote.votingUser,
            hasVoted: true
          }];

        }

        const candidate = this.candidates.find(candidate => candidate.user.id === vote.votedUser.id)
        if (candidate) {
          candidate.votes++;
        } else {
          this.candidates = [...this.candidates, {
            user: vote.votedUser,
            votes: 1
          }];
        }
        this.cdr.detectChanges();
      })
    ).subscribe();
  }

  private fetchVotes() {
    forkJoin([
      this.votingService.getAllVotes$(),
      this.userService.getAllUsers$()
    ]).pipe(
      tap(([votes, users]) => {
        this.voters = users.map(user => ({
          user: user,
          hasVoted: votes.some(vote => vote.votingUser.id === user.id)
        }));

        this.candidates = users.map(user => ({
          user,
          votes: votes.filter(vote => vote.votedUser.id === user.id).length
        })).filter(candidate => candidate.votes > 0);
        this.cdr.detectChanges();
      })
    ).subscribe()
  }
}
