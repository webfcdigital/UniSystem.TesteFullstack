import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { UserRegisterComponent } from './users/user-register/user-register';
import { UserListComponent } from './users/user-list/user-list';

const routes: Routes = [
  { path: 'register', component: UserRegisterComponent },
  { path: 'users', component: UserListComponent },
  { path: '', redirectTo: '/register', pathMatch: 'full' }, // Default route
];

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient()
  ]
};
