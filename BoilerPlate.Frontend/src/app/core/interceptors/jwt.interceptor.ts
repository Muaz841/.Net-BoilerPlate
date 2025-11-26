import {
  HttpEvent,
  HttpHandlerFn,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const jwtInterceptor = (
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> => {
  const authService = inject(AuthService);
  const router = inject(Router);

  let authReq = req;
  
  if (authService.isLoggedIn() && authService.getToken()) {
    authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${authService.getToken()}`
      }
    });
  }

  return next(authReq).pipe(
    tap({
      error: (err: any) => {
        if (err instanceof HttpErrorResponse && err.status === 401) {          
          authService.logout();
          router.navigate(['/auth']);
        }
      }
    })
  );
};