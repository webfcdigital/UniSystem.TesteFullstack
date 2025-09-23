import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7001/api/auth'; // Adjust if your backend URL is different

  constructor(private http: HttpClient) { }

  login(credentials: { email: string; password: string }): Observable<string> {
    return this.http.post(this.apiUrl + '/login', credentials, { responseType: 'text' }).pipe(
      tap(token => {
        localStorage.setItem('jwt_token', token);
      })
    );
  }

  getToken(): string | null {
    return localStorage.getItem('jwt_token');
  }

  logout(): void {
    localStorage.removeItem('jwt_token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}