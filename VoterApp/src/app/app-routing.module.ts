import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VotingComponent } from './components/voting/voting.component';

const routes: Routes = [
  { path: '', redirectTo: 'Voting', pathMatch: 'full' },
  { path: 'Voting', component: VotingComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
