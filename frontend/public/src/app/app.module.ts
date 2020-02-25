import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './Components/NavMenu/nav-menu/nav-menu.component';
import { HomeComponent } from './Components/home/home.component';
import { LayoutComponent } from './Components/layout/layout.component';
import { UserProfileComponent } from './Components/Users/user-profile/user-profile.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {JwtTokenInterceptor} from './Api/jwt-token-interceptor';
import {AuthorizeGuard} from './Guards/authorize.guard';
import { LoginComponent } from './Components/login/login.component';
import {ReactiveFormsModule} from '@angular/forms';
import { MyProfileComponent } from './Components/Users/my-profile/my-profile.component';
import { UsersTableComponent } from './Components/Users/users-table/users-table.component';
import { FollowersTableComponent } from './Components/Users/followers-table/followers-table.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LayoutComponent,
    UserProfileComponent,
    LoginComponent,
    MyProfileComponent,
    UsersTableComponent,
    FollowersTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: JwtTokenInterceptor,
    multi: true
  },
  AuthorizeGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
