import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import {
  LoginRequest,
  RegisterRequest,
  ResetPasswordRequest,
} from '../models/auth.request.model';
import {
  AcknowledgeUserResponse,
  LoginResponse,
  RegisterResponse,
  ResetPasswordResponse,
} from '../models/auth.response.model';

@Injectable({
  providedIn: 'root',
})
export class AuthApiService {
  private http = inject(HttpClient);
  private baseUrl: string = environment.authUrl;

  constructor() {}

  login(login: LoginRequest): Observable<LoginResponse> {
    const url = `${this.baseUrl}/auth/users/login`;
    console.log('Login request:', login);
    console.log(url);
    return this.http.post<LoginResponse>(url, login);
  }

  register(registerData: RegisterRequest): Observable<RegisterResponse> {
    const url = `${this.baseUrl}/auth/users/register`;
    console.log('Register request:', registerData);
    console.log(url);
    return this.http.post<RegisterResponse>(url, registerData);
  }

  acknowledgeUser(tokenUrl: string): Observable<AcknowledgeUserResponse> {
    return this.http.get<AcknowledgeUserResponse>(tokenUrl);
  }

  logout(): Observable<any> {
    const url = `${this.baseUrl}/home`;
    return this.http.post(url, this.logout);
  }

  // logout(): Observable<any> {
  //   const url = `${this.authUrl}/users/logout`;
  //   return this.http.post(url, {});
  // }

  acknowledgePassword(tokenUrl: string): Observable<AcknowledgeUserResponse> {
    const url = `${this.baseUrl}/auth/password/acknowledge`;
    return this.http.get<AcknowledgeUserResponse>(tokenUrl);
  }

  resetPassword(
    token: ResetPasswordRequest
  ): Observable<ResetPasswordResponse> {
    const url = `${this.baseUrl}/auth/password/reset`;
    return this.http.post<ResetPasswordResponse>(url, token);
  }

  // getUsers() {
  //     const url = this.baseUrl + '/users';
  //     return this.http.get(url);
  // }

  // getRoles() {}
  // refreshToken() {
  //     const url = this.baseUrl + '/refresh';
  //     return this.http.post(url, null);
  // }
}
