import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { UserService } from '../../../core/services/user.service';
import { LoginRequest } from '../../../core/services/Interfaces/loginRequest.interface';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-login',
  imports: [ FormsModule],
  standalone: true,  
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginInput: LoginRequest = { email: '', password: '' };
  constructor(
    private authService: AuthService,
    private router: Router,
    private userService: UserService,
  ) {}

  
 login(userInput: LoginRequest):  void {    
  if (!userInput.email?.trim() || !userInput.password?.trim()) {
  return alert("Invalid input");
}
  this.authService.login(userInput).subscribe({
    next: () => {     
      setTimeout(() => {           
        if (this.authService.isLoggedIn()) {
       
          this.router.navigate(['/dashboard']);
        } else {
          console.error('Token not set, navigation blocked.');
        }
      }, 0)
    },
    error: (err) => alert('Login failed: ' + (err.error?.message || 'Invalid credentials'))
  });
}
  
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/auth']);
  }

getallusers() : void{
   this.userService.getAll()
}

}
