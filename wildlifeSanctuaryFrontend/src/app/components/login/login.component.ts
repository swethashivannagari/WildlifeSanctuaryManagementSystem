import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { response } from 'express';
import { Router } from '@angular/router';
import { error } from 'console';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  loginForm: FormGroup;
  errorMessage: string = "";

  constructor(private fb: FormBuilder, private authservice: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onLogin(): void {
    if (this.loginForm?.invalid) {
      this.errorMessage = 'Please fill the form correctly.'
      return;
    }


    const { email, password } = this.loginForm.value;
    console.log(email, password);
    this.authservice.login(email, password).subscribe(

      (response) => {
        console.log(response);
        if (response?.token) {
          this.authservice.storeToken(response.token);
          console.log("logged in");
          const userRole = localStorage.getItem('userRole');
          console.log(userRole)
          if (userRole) {
            this.navigateToRoleBasedRoute(userRole);
          } else {
            alert("Unable to determine user role.");
          }
        }

      },
      (error) => {
        this.errorMessage = "Invalid login Credentials.Try Again!!"
      }
    )
  }

  private navigateToRoleBasedRoute(role: string): void {
    console.log(role, "navigate");
    switch (role) {
      case 'Admin':
        this.router.navigate(['/main/dashboard']);
        break;
      case 'Manager':
        this.router.navigate(['/main/dashboard']);
        break;
      case 'Ranger':
        this.router.navigate(['/ranger']);
        break;
      case 'Veterinarian':
        this.router.navigate(['/veterinarian']);
        break;
      case 'Biologist':
        this.router.navigate(['/biologist']);
        break;
      case 'Conservationist':
        this.router.navigate(['/conservationist']);
        break;
      default:
        this.router.navigate(['/login']); // Redirect to login if role is unknown
        this.errorMessage = "Invalid role detected.";
        break;
    }
  }
}
