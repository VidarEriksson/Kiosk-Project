import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { UserComponent } from './components/user/user.component';
import { AuthComponent } from './components/auth/auth.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'user', component: UserComponent },
  //   {
  //     path: 'categories',
  //     canMatch: [AuthGuard],
  //     component: ListCategoriesComponent,
  //   },
  {
    path: 'auth',
    children: [
      { path: 'login', component: AuthComponent },
      { path: 'register', component: AuthComponent },
      { path: 'password/reset', component: ResetPasswordComponent },
    ],
  },
];
