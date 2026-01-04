import { Routes } from '@angular/router';
import { MembersListComponent } from './features/members/members-list/members-list.component';

export const routes: Routes = [
    {path:'', redirectTo:'members', pathMatch:'full'},
    {path:'members', component: MembersListComponent}
];
