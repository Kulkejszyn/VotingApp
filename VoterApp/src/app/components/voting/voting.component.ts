import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users/users.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { User } from '../../models/users/user';
import { VotesService } from '../../services/votes/votes.service';

@Component({
  selector: 'app-voting',
  templateUrl: './voting.component.html',
  styleUrls: ['./voting.component.scss']
})
export class VotingComponent implements OnInit {
  public users$ = this.userService.getAllUsers$();

  public voteForm = new FormGroup({
    voter: new FormControl<User | undefined>(undefined, [Validators.required]),
    candidate: new FormControl<User | undefined>(undefined, [Validators.required])
  });

  constructor(private userService: UsersService,
              private voteService: VotesService) {
  }

  ngOnInit(): void {

  }

  public vote() {
    this.voteForm.markAllAsTouched();
    if (!this.voteForm.valid) return;

    const voter = this.voteForm.value.voter!;
    const candidate = this.voteForm.value.candidate!;

    this.voteService.addVote$({
      votedUserId: candidate.id,
      votingUserId: voter.id
    }).subscribe();
  }
}
