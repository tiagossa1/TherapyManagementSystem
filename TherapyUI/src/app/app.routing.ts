import { Routes, RouterModule } from "@angular/router";
import { ModuleWithProviders } from "@angular/core";
import { HomeComponent } from "./components/home/home.component";
import { AuthGuardService } from "./services/auth-guard.service";

const routes: Routes = [
  {
    path: "login",
    loadChildren: "./authentication/authentication.module#AuthenticationModule"
  },
  {
    path: "billings/new-billing",
    loadChildren: "./billings/billings.module#BillingsModule"
  },
  {
    path: "billings/view-billings",
    loadChildren: "./billings/billings.module#BillingsModule"
  },
  { path: "index", component: HomeComponent, canActivate: [AuthGuardService] },
  { path: "", redirectTo: "index", pathMatch: "full" },
  { path: "**", redirectTo: "index", pathMatch: "full" }
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
