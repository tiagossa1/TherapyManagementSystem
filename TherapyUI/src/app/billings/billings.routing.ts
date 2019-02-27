import { Routes, RouterModule } from "@angular/router";
import { ViewBillingsComponent } from "./view-billings/view-billings.component";
import { ModuleWithProviders } from "@angular/core";
import { AuthGuardService } from "../services/auth-guard.service";

const routes: Routes = [
  {
    path: "billings/view-billings",
    component: ViewBillingsComponent,
    canActivate: [AuthGuardService]
  },
  { path: "billings", redirectTo: "billings/view-billings", pathMatch: "full" }
];

export const BillingsRoutes: ModuleWithProviders = RouterModule.forChild(
  routes
);
