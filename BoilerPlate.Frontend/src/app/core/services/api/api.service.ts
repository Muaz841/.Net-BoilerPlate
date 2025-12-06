import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ApiService {
  protected readonly apiUrl = environment.apiUrl;

  http = inject(HttpClient);

  protected get<T>(url: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}${url}`);
  }

  protected post<T>(url: string, body: any): Observable<T> {    
    return this.http.post<T>(`${this.apiUrl}${url}`, body);
  }

  protected put<T>(url: string, body: any): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}${url}`, body);
  }

    protected delete<T>(url: string): Observable<T> {
      return this.http.delete<T>(`${this.apiUrl}${url}`);
    }
}