import { Routes, RouterModule } from "@angular/router";
import { AuthGuardService } from "../services/auth-guard.service";
import { ViewAppointmentsComponent } from "./view-appointments/view-appointments.component";
import { ModuleWithProviders } from "@angular/core";

const routes: Routes = [
  {
    path: "appointments/view-appointments",
    component: ViewAppointmentsComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: "appointments",
    redirectTo: "appointments/view-appointments",
    pathMatch: "full"
  }
];

export const AppointmentsRoutes: ModuleWithProviders = RouterModule.forChild(
  routes
);
