import { ApplicationConfig } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { UserRegister } from './users/user-register/user-register';
import { UserList } from './users/user-list/user-list';
import { LoginComponent } from './login/login';
import { authGuard } from './auth.guard';
import { authInterceptor } from './auth.interceptor';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: UserRegister },
  { path: 'users', component: UserList, canActivate: [authGuard] },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([authInterceptor])),
  ]
};

export const API_BASE_URL = 'https://legendary-disco-jgqp6g94p9vhp7rr-5183.app.github.dev';