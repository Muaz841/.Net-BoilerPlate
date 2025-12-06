import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { RegisterRequest } from '../../core/services/Interfaces/registerRequest.interface';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerInput: RegisterRequest = { email: '', name: '', password: '' };
  loading = false;

  constructor(private authService: AuthService, private router: Router) {}

  register(): void {
    if (!this.registerInput.email.trim() || !this.registerInput.name.trim() || !this.registerInput.password.trim()) {
      alert('All fields are required.');
      return;
    }
    this.loading = true;
    this.authService.register(this.registerInput).subscribe({
      next: () => {
        alert('Registration successful. Please login.');
        this.router.navigate(['/auth']);
      },
      error: (err: { error: { message: any; }; }) => {
        alert('Registration failed: ' + (err.error?.message || 'Unknown error'));
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }
}
