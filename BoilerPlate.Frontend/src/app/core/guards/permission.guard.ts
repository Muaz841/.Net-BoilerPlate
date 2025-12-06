import { CanMatchFn, Router, UrlTree } from '@angular/router';
import { inject } from '@angular/core';
import { PermissionService } from '../services/permission/permission.service';
import { map } from 'rxjs';

export const permissionGuard = (requiredPermission: string): CanMatchFn => {
  return () => {
    const permissionService = inject(PermissionService);
    const router = inject(Router);

    return permissionService.permissions$.pipe(
      map(perms => 
        perms.includes(requiredPermission)
          ? true
          : router.createUrlTree(['/forbidden'])   
      )
    );
  };
};