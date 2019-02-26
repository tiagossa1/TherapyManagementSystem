import { Component, OnInit, ViewChild } from "@angular/core";
import { BillingService } from "../services/billing.service";
import { Billing } from "src/app/models/Billing";
import {
  MatTableDataSource,
  MatSort,
  MatSortable,
  MatDialog
} from "@angular/material";
import { NewBillingComponent } from "../new-billing/new-billing.component";

@Component({
  selector: "app-view-billings",
  templateUrl: "./view-billings.component.html",
  styleUrls: ["./view-billings.component.scss"]
})
export class ViewBillingsComponent implements OnInit {
  billings;
  @ViewChild(MatSort) sort: MatSort;

  displayedColumns: string[] = [
    "clientName",
    "clientEmail",
    "appointmentDate",
    "appointmentTypeName",
    "therapistName",
    "price"
  ];
  constructor(
    private billingService: BillingService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.getBillings();
  }

  getBillings() {
    this.billingService.get().subscribe(data => {
      this.billings = new MatTableDataSource(data as Billing[]);
      this.billings.sort = this.sort;
    });
  }

  openDialog() {
    const dialogRef = this.dialog.open(NewBillingComponent, {
      width: "500px",
      height: "400px"
    });

    dialogRef.afterClosed().subscribe(res => {
      this.getBillings();
    });
  }
}
