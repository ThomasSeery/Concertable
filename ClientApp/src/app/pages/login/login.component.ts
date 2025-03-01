import { Component } from '@angular/core';
import { LoginCredentials } from '../../models/login-credentials';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  
  credentials: LoginCredentials = {
    email: '',
    password: ''
    };
  
  constructor(private authService: AuthService, private router: Router) { }

  login() {
    this.authService.login(this.credentials)
      .subscribe();
  }
}
