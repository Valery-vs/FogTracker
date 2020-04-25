import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidationErrors } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: 'login.component.html'
})
export class LoginComponent {
  public isLogging: boolean;

  public loginForm = new FormGroup({
    login: new FormControl('', this.isNullOrWhiteSpaceValidator),
    password: new FormControl('', this.isNullOrWhiteSpaceValidator),
  });

  public onSubmit() {
    this.isLogging = true;
    console.warn(this.loginForm.value);
  }

  private isNullOrWhiteSpaceValidator(control: FormControl) {
    return control.value && control.value !== '' && control.value.trim().length > 0 ? null : { 'isNullOrWhiteSpace': true };
  }
}
