import { Injectable } from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
import {ApiService} from '../Api/api.service';
import {Routes} from '../Routes/routes';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeGuard implements CanActivate {
  constructor(private api: ApiService, private router: Router) {
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) {
    const currentUser = this.api.currentUserValue;
    if (currentUser) {
      return true;
    }

    this.router.navigate([Routes.login]);
    return false;
  }
}
