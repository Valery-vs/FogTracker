import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthenticationService {
  constructor(private http: HttpClient, @Inject('API_URL') private apiUrl: string) {
  }

  public login(username: string, password: string): Promise<any> {
    return this.http.post<any>(`${this.apiUrl}/account/login`, { username, password })
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        // localStorage.setItem('currentUser', JSON.stringify(user));
        // this.currentUserSubject.next(user);
        return user;
      })).toPromise();
  }

  public logout() {

  }
}
