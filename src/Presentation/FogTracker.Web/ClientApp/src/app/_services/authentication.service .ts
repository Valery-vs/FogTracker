import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthenticationService {
  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string) {
  }

  public login(username: string, password: string): Promise<any> {
    return this.http.post<string>(`${this.apiUrl}/account/login`, { 'UserName': username, 'Password': password })
      .pipe(map(token => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        // localStorage.setItem('currentUser', JSON.stringify(user));
        // this.currentUserSubject.next(user);
        return token;
      })).toPromise();
  }

  public logout() {

  }
}
