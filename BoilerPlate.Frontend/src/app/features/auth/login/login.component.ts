import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { UserService } from '../../../core/services/user.service';
import { LoginRequest } from '../../../core/services/Interfaces/loginRequest.interface';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule, RouterModule],
  standalone: true,  
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginInput: LoginRequest = { email: '', password: '' };
  loginError: string | null = null;

  constructor(
    private authService: AuthService,
    private router: Router,
    private userService: UserService,
  ) {}

  
 login(userInput: LoginRequest):  void {    
  this.loginError = null;
  if (!userInput.email?.trim() || !userInput.password?.trim()) {
  this.loginError = 'Invalid input.';
  return;
}
  this.authService.login(userInput).subscribe({
    next: () => {     
      setTimeout(() => {           
        if (this.authService.isLoggedIn()) {
       
          this.router.navigate(['/dashboard']);
        } else {
          this.loginError = 'Token not set, navigation blocked.';
        }
      }, 0)
    },
    error: (err) => {
      if (err.error?.message?.toLowerCase().includes('not found')) {
        this.loginError = 'User not found. Please register.';
      } else {
        this.loginError = 'Login failed: ' + (err.error?.message || 'Invalid credentials');
      }
    }
  });
}
  
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }

  

getallusers() : void{
   this.userService.getAll();
}

}
