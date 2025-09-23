import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface User {
  id: string;
  name: string;
  email: string;
}

export interface UserRegistrationDto {
  name: string;
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7001/api/users'; // Adjust if your backend URL is different

  constructor(private http: HttpClient) { }

  registerUser(userDto: UserRegistrationDto): Observable<User> {
    return this.http.post<User>(this.apiUrl, userDto);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }
}