import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class AuthenticationService {
  public loggedIn = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string) {
    if (localStorage.getItem('token') != null) {
      this.loggedIn.next(true);
    }
  }

  public login(username: string, password: string): Promise<any> {
    return this.http.post<any>(`${this.apiUrl}/account/login`, { 'UserName': username, 'Password': password })
      .pipe(
        map(res => {
        localStorage.setItem('token', res.token);
        this.loggedIn.next(true);
        return true;
      })).toPromise();
  }

  public logout() {
    localStorage.removeItem('token');
    this.loggedIn.next(false);
  }
}
