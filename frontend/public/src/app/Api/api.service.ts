import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Router} from '@angular/router';
import {Routes} from '../Routes/routes';
import {map} from 'rxjs/operators';
import {BehaviorSubject, Observable} from 'rxjs';
import {UserProfileData} from '../Models/UserProfileData';

interface ILoginModel {
  token: string;
  returnUrl: string;
}

interface IUser {
  login: string;
  id: number;
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private currentUserSubject: BehaviorSubject<IUser>;
  private currentUser: Observable<IUser>;
  constructor(private http: HttpClient, private router: Router) {
    this.currentUserSubject = new BehaviorSubject<IUser>(JSON.parse(localStorage.getItem(environment.apiTokenKey)));
    this.currentUser = this.currentUserSubject.asObservable();
  }
  public login(login: string, password: string) {
    const body = {
      login,
      password
    };

    return this.http.post<any>(`${environment.backendUrl}/api/authorize`, body)
      .pipe(map(res => {
        localStorage.setItem(environment.apiTokenKey, JSON.stringify(res));
        console.log(res);
        return res;
      }));
  }
  public get currentUserValue() {
    return this.currentUserSubject.value;
  }
  public logout() {
    localStorage.removeItem(environment.apiTokenKey);
    this.currentUserSubject.next(null);
  }
  public getUserProfileData(id: number): Observable<UserProfileData> {
    return this.http.get<UserProfileData>(`${environment.backendUrl}/api/users/profile/${id}`);
  }
  public getMyUserProfileData(): Observable<UserProfileData> {
    return this.http.get<UserProfileData>(`${environment.backendUrl}/api/users/myProfile`);
  }
}
