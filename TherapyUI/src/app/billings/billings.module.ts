import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BillingsComponent } from "./billings.component";
import { BillingsRoutes } from "./billings.routing";
import { NewBillingComponent } from "./new-billing/new-billing.component";
import { ViewBillingsComponent } from "./view-billings/view-billings.component";

@NgModule({
  imports: [CommonModule, BillingsRoutes],
  declarations: [BillingsComponent, NewBillingComponent, ViewBillingsComponent],
  exports: [NewBillingComponent, ViewBillingsComponent]
})
export class BillingsModule {}
