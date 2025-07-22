import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class Login {
  email: string = '';
  password: string = '';
  errorMessage: string = '';


  private adminCredentials = {
    email: 'admin',
    password: 'admin123'
  };

  constructor(private router: Router) {}

  onSubmit() {
    if (this.email === this.adminCredentials.email && this.password === this.adminCredentials.password) {
      localStorage.setItem('isAuthenticated', 'true');
      this.router.navigate(['/admin-dashboard']);
    } else {
      this.errorMessage = 'Credenciales incorrectas. Por favor, inténtalo de nuevo.';
    }
  }
}