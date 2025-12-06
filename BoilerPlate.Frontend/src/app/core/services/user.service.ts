import { Injectable } from '@angular/core';
import { ApiService } from './api/api.service'; 
import { Observable } from 'rxjs';

export interface UserDto {
  id: string;
  email: string;
  name: string;
  isActive: boolean;
  roles: string[];
  creationTime: string;
}

export interface CreateUserRequest {
  email: string;
  name: string;
  password: string;
}

export interface UpdateUserRequest {
  name: string;
  isActive: boolean;
}

@Injectable({
  providedIn: 'root'
})

export class UserService extends ApiService {
  
  getAll(): Observable<UserDto[]> {
    return this.get<UserDto[]>('/api/users/GetAll');
  }
  
  getById(id: string): Observable<UserDto> {
    return this.get<UserDto>(`/api/users/${id}/GetById`);
  }
  
  create(request: CreateUserRequest): Observable<UserDto> {
    return this.post<UserDto>('/api/users/create', request);
  }

  // PUT /api/users/{id}
  update(id: string, request: UpdateUserRequest): Observable<any> {
    return this.put(`/api/users/${id}`, request);
  }

}