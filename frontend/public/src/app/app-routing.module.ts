import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HomeComponent} from './Components/home/home.component';
import {UserProfileComponent} from './Components/Users/user-profile/user-profile.component';
import {AuthorizeGuard} from './Guards/authorize.guard';
import {Routes as routeList} from './Routes/routes';
import {LoginComponent} from './Components/login/login.component';
import {MyProfileComponent} from './Components/Users/my-profile/my-profile.component';
import {FollowersTableComponent} from './Components/Users/followers-table/followers-table.component';


const routes: Routes = [
  {path: routeList.home , pathMatch: 'full', component: HomeComponent, canActivate: [AuthorizeGuard]},
  {path: routeList.userProfile, component: UserProfileComponent, canActivate: [AuthorizeGuard]},
  {path: routeList.login, component: LoginComponent},
  {path: routeList.myProfile, component: MyProfileComponent, canActivate: [AuthorizeGuard]},
  {path: routeList.followers, component: FollowersTableComponent, canActivate: [AuthorizeGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
