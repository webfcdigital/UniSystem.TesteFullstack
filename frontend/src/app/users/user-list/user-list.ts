import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user';
import { AuthService } from '../../services/auth';
import { CommonModule } from '@angular/common';

interface User {
  id: string;
  name: string;
  email: string;
}

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-list.html',
  styleUrls: ['./user-list.scss']
})
export class UserList implements OnInit {
  users: User[] = [];
  error: string | null = null;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data;
      },
      error: (err) => {
        this.error = 'Failed to load users. You might be unauthorized.';
        console.error(err);
      }
    });
  }

  logout(): void {
    this.authService.logout();
  }
}