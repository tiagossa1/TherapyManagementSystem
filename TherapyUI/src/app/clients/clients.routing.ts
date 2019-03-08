import { Routes, RouterModule } from "@angular/router";
import { ViewClientsComponent } from "./view-clients/view-clients.component";
import { AuthGuardService } from "../services/auth-guard.service";
import { ModuleWithProviders } from "@angular/core";

const routes: Routes = [
  {
    path: "clients/view-clients",
    component: ViewClientsComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: "clients",
    redirectTo: "clients/view-clients",
    pathMatch: "full"
  }
];

export const ClientsRoutes: ModuleWithProviders = RouterModule.forChild(routes);
