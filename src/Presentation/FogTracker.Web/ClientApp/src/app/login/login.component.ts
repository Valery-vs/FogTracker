import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthenticationService } from '../_services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit{
  public isLoginProcessActive: boolean;
  public loginErrors: string;
  private returnUrl: string;

  public loginForm = new FormGroup({
    username: new FormControl('', this.isNullOrWhiteSpaceValidator),
    password: new FormControl('', this.isNullOrWhiteSpaceValidator),
  });

  constructor(private authService: AuthenticationService, private route: ActivatedRoute,
    private router: Router) {}

  public ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    this.authService.logout();
  }

  public onSubmit() {
    this.isLoginProcessActive = true;
    this.loginErrors = null;
    this.authService.login(this.loginForm.value.username, this.loginForm.value.password).then(result => {
        this.isLoginProcessActive = false;

        this.router.navigateByUrl(this.returnUrl);
      })
      .catch(response => {
        console.error(response);
        this.isLoginProcessActive = false;
        this.loginErrors = response.error.errorMessage;
      });
  }

  private isNullOrWhiteSpaceValidator(control: FormControl) {
    return control.value && control.value !== '' && control.value.trim().length > 0 ? null : { 'isNullOrWhiteSpace': true };
  }
}
