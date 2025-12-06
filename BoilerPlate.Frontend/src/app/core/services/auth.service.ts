import {  Injectable } from '@angular/core';
import { Observable, switchMap, tap } from 'rxjs';
import { Router } from '@angular/router';
import { PermissionService } from './permission/permission.service';
import { LoginResponse } from './Interfaces/loginResponse.interface';
import { LoginRequest } from './Interfaces/loginRequest.interface';
import { ApiService } from './api/api.service';
import { RegisterRequest } from './Interfaces/registerRequest.interface';


@Injectable({
  providedIn: 'root'
})

export class AuthService  extends ApiService{
  private readonly TOKEN_KEY = 'access_token';
 

 constructor(private router: Router, private permissionService: PermissionService) {
   super();
   
  }

login(credentials: LoginRequest): Observable<LoginResponse> {
  return this.post<LoginResponse>('api/auth/login', credentials).pipe(
    tap(response => {
      console.log("LOGIN RESPONSE:", response);
      this.handleSuccessLogin(response);
    }),
    tap(response => {
      this.permissionService.loadPermissions().subscribe({
        next: (perms) => console.log('Permissions loaded:', perms),
        error: (err) => console.warn('Failed to load permissions:', err)
      });
    }),
    tap(() => this.router.navigate(['/dashboard']))
  );
} 

  register(data: RegisterRequest): Observable<any> {
    return this.post('/api/users', data);
  }


 logout(): void {
  debugger;
    localStorage.removeItem(this.TOKEN_KEY);    
    this.router.navigate(['/auth']);
  }
  

  isLoggedIn(): boolean {
  const token = this.getToken();
  if (!token) return false;
  const expires = localStorage.getItem('expires');
  if (!expires) return false;
  return new Date(expires).getTime() > Date.now();
}


  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  // getCurrentUser(): CurrentUser | null {
  //   const json = localStorage.getItem(this.USER_KEY);
  //   return json ? JSON.parse(json) : null;
  // }

  // hasPermission(permission: string): boolean {
  //   const user = this.getCurrentUser();
  //   return user?.permissions.includes(permission) ?? false;
  // }

  private handleSuccessLogin(response: LoginResponse): void {      
    localStorage.setItem(this.TOKEN_KEY, response.accessToken);
    localStorage.setItem('expires', response.expires);
  }
}