import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ResetPasswordRequest } from '../../models/auth.request.model';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    CommonModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
})
export class ResetPasswordComponent {
  //#region properties
  resetPasswordError: boolean = false;
  ResetPasswordForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
    confirmPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
  });
  //#endregion

  //#region constructor
  authService = inject(AuthService);
  router = inject(Router);
  //#endregion

  async onResetPasswordSubmit() {
    console.log('Reset Password mode');
    if (
      this.ResetPasswordForm.valid &&
      this.ResetPasswordForm.value.password ===
        this.ResetPasswordForm.value.confirmPassword
    ) {
      try {
        const request: ResetPasswordRequest = {
          email: this.ResetPasswordForm.value.email as string,
          password: this.ResetPasswordForm.value.password as string,
          confirmPassword: this.ResetPasswordForm.value
            .confirmPassword as string,
        };
        const response = await this.authService.resetPassword(request);
        console.log('reset password  successfully', response);
        if (response) {
          await this.authService.acknowledgeNewPassword(response.url);
          console.log('Account confirmed successfully');
          this.router
            .navigateByUrl('/', { skipLocationChange: true })
            .then(() => {
              this.router.navigate(['/auth/login']);
            });
        }
      } catch (error) {
        console.error('Registration Error', error);
        this.resetPasswordError = true;
      }
    } else {
      console.log('Form is not valid or passwords do not match');
      this.resetPasswordError = true;
    }
  }

  onNoClick(): void {
    this.router.navigate(['/auth/login']);
  }
}
