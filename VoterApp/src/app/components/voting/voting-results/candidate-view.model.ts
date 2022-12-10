import { User } from '../../../models/users/user';

export interface CandidateViewModel {
  user: User;
  votes: number;
}
