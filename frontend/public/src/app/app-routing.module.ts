import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HomeComponent} from './Components/home/home.component';
import {UserProfileComponent} from './Components/user-profile/user-profile.component';
import {AuthorizeGuard} from './Guards/authorize.guard';


const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeComponent},
  {path: 'users/profile', component: UserProfileComponent, canActivate: [AuthorizeGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
