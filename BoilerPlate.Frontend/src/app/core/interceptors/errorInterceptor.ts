import {
  HttpEvent,
  HttpHandlerFn,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

export const errorInterceptor = (
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {        
        authService.logout();
        router.navigate(['/auth']);
      }

      if (error.status === 403) {        
        alert('Access denied: You do not have permission to perform this action.');        
      }

      if (error.status === 500) {
        alert('Server error. Please try again later.');
      }

      if (!error.status) {    
        alert('Cannot connect to server. Check your connection.');
      }
      
      return throwError(() => error);
    })
  );
};