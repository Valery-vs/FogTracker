import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidationErrors } from '@angular/forms';
import { AuthenticationService } from '../_services/authentication.service ';

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html'
})
export class LoginComponent {
  public isLoginProcessActive: boolean;
  public loginErrors: string;

  public loginForm = new FormGroup({
    login: new FormControl('', this.isNullOrWhiteSpaceValidator),
    password: new FormControl('', this.isNullOrWhiteSpaceValidator),
  });

  constructor(private authService: AuthenticationService) {}

  public onSubmit() {
    this.isLoginProcessActive = true;
    console.warn(this.loginForm.value);
    this.authService.login(this.loginForm.value.username, this.loginForm.value.password).then(user => {
        this.isLoginProcessActive = false;
        this.loginErrors = null;
      })
      .catch(error => {
        this.isLoginProcessActive = false;
        this.loginErrors = error;
      });
  }

  private isNullOrWhiteSpaceValidator(control: FormControl) {
    return control.value && control.value !== '' && control.value.trim().length > 0 ? null : { 'isNullOrWhiteSpace': true };
  }
}
