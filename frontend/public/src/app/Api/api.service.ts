import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Router} from '@angular/router';
import {Routes} from '../Routes/routes';
import {map} from 'rxjs/operators';

interface ILoginModel {
  token: string;
  returnUrl: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private backendUrl: 'http://localhost:5000';
  constructor(private http: HttpClient, private router: Router) { }
  public check(): Promise<any> {
    return this.http.get(this.backendUrl).toPromise();
  }
  public login() {
    const body = {
      login: 'Admin',
      password: '123'
    };

    return this.http.post<any>(`${environment.backendUrl}/api/authorize`, body)
      .pipe(map(res => {
        localStorage.setItem(environment.apiTokenKey, JSON.stringify(res));
        console.log(res);
        return res;
      }));
  }
  public get currentUser() {
    return {
      token: JSON.parse(localStorage.getItem(environment.apiTokenKey))
    };
  }
}
