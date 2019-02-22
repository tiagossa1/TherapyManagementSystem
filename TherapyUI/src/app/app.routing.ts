import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';

const routes: Routes = [
  { path: 'login', loadChildren: './authentication/authentication.module#AuthenticationModule' },
];

export const AppRoutes : ModuleWithProviders = RouterModule.forRoot(routes);
