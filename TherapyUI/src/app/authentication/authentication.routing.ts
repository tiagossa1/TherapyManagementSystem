import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ModuleWithProviders } from '@angular/core';
import { LoginGuardService } from '../services/login-guard.service';

const routes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [LoginGuardService] },
  { path: 'register', component: RegisterComponent }
];

export const AuthenticationRoutes: ModuleWithProviders = RouterModule.forChild(routes);
