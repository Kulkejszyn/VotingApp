import { User } from '../users/user';

export interface Vote {
  votingUser: User;
  votedUser: User;
}
