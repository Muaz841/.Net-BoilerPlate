import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ApiService } from '../api/api.service';

@Injectable({ providedIn: 'root' })
export class PermissionService  extends ApiService{
  private permissions = new BehaviorSubject<string[]>([]);
  permissions$ = this.permissions.asObservable();

  loadPermissions() {
    return this.get<string[]>(`${environment.apiUrl}/api/permissions/me`).pipe(
      tap(perms => this.permissions.next(perms))
    );
  }

  has(permission: string): boolean {
    return this.permissions.value.includes(permission);
  }

  has$(permission: string) {
    return this.permissions$.pipe(
      map(perms => perms.includes(permission))
    );
  }
}