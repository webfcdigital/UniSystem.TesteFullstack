import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-register.html',
  styleUrls: ['./user-register.scss']
})
export class UserRegister {
  registerForm: FormGroup;
  error: string | null = null;
  success = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      this.userService.createUser(this.registerForm.value).subscribe({
        next: () => {
          this.success = true;
          this.error = null;
          this.registerForm.reset();
          setTimeout(() => this.router.navigate(['/login']), 2000);
        },
        error: (err) => {
          this.error = 'Email already exists or another error occurred.';
          this.success = false;
          console.error(err);
        }
      });
    }
  }
}