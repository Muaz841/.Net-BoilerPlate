import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  imports: [RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  
  login(): void {
    this.authService.login();
    this.router.navigate(['/dashboard']);
  }
  
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }
}
