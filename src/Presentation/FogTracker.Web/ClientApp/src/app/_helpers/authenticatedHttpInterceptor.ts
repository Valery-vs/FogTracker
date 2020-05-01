import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../_services/authentication.service';

@Injectable()
export class AuthenticatedHttpInterceptor implements HttpInterceptor {

    constructor(private router: Router, private authService: AuthenticationService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let request = req;
        const token = localStorage.getItem('token');
        if (token != null) {
            request = req.clone({
                headers: req.headers.set('Authorization', 'Bearer ' + token)
            });
        }

        return next.handle(request).pipe(catchError(err => this.catchErrors(err, this.router)));
    }

    private catchErrors(err: HttpErrorResponse, router: Router): Promise<any> {
        if (err.status === 0) {
            // this.router.navigate(['/underconstruction'], { skipLocationChange: true });
        } else if ((err.status === 401 || err.status === 403) && router.url !== '/login') {
            console.error('authorization errors, move to login');

            this.authService.logout();
            this.router.navigate(['/login'], { queryParams: { returnUrl: router.url } });
        }

        return Promise.reject(err);
    }
}
