import { Routes, RouterModule } from "@angular/router";
import { NewBillingComponent } from "./new-billing/new-billing.component";
import { ViewBillingsComponent } from "./view-billings/view-billings.component";
import { ModuleWithProviders } from "@angular/core";

const routes: Routes = [
  { path: "billings/new-billing", component: NewBillingComponent },
  { path: "billings/view-billings", component: ViewBillingsComponent },
  { path: "billings", redirectTo: "billings/view-billings", pathMatch: "full" }
];

export const BillingsRoutes: ModuleWithProviders = RouterModule.forChild(
  routes
);
