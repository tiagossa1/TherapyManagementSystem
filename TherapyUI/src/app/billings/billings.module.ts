import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BillingsComponent } from "./billings.component";
import { BillingsRoutes } from "./billings.routing";
import { NewBillingComponent } from "./new-billing/new-billing.component";
import { ViewBillingsComponent } from "./view-billings/view-billings.component";
import { TranslateModule } from "@ngx-translate/core";

import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import { MatSortModule } from "@angular/material/sort";
import { MatDialogModule } from "@angular/material/dialog";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { ReactiveFormsModule } from "@angular/forms";

@NgModule({
  imports: [
    CommonModule,
    BillingsRoutes,
    MatTableModule,
    MatButtonModule,
    MatSortModule,
    MatDialogModule,
    MatInputModule,
    MatSelectModule,
    TranslateModule,
    ReactiveFormsModule
  ],
  declarations: [BillingsComponent, NewBillingComponent, ViewBillingsComponent],
  entryComponents: [NewBillingComponent],
  exports: [NewBillingComponent, ViewBillingsComponent]
})
export class BillingsModule {}
