import { User } from '../../../models/users/user';

export interface VoterViewModel {
  user: User;
  hasVoted: boolean;
}
