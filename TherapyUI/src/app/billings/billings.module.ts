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
import { MatIconModule } from "@angular/material/icon";
import { MatTooltipModule } from "@angular/material/tooltip";

import { ReactiveFormsModule } from "@angular/forms";
import { EditBillingComponent } from "./edit-billing/edit-billing.component";

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
    MatIconModule,
    MatTooltipModule,
    TranslateModule,
    ReactiveFormsModule
  ],
  declarations: [BillingsComponent, NewBillingComponent, ViewBillingsComponent, EditBillingComponent],
  entryComponents: [NewBillingComponent, EditBillingComponent],
  exports: [NewBillingComponent, ViewBillingsComponent, EditBillingComponent]
})
export class BillingsModule {}
