import { Component, OnInit, ViewChild } from "@angular/core";
import { BillingService } from "../services/billing.service";
import { Billing } from "src/app/models/Billing";
import {
  MatTableDataSource,
  MatSort,
  MatDialog
} from "@angular/material";
import { NewBillingComponent } from "../new-billing/new-billing.component";

@Component({
  selector: "app-view-billings",
  templateUrl: "./view-billings.component.html",
  styleUrls: ["./view-billings.component.scss"]
})
export class ViewBillingsComponent implements OnInit {
  billings: Billing[];
  dataSource;
  @ViewChild(MatSort) sort: MatSort;

  displayedColumns: string[] = [
    "appointmentDate",
    "appointmentTypeName",
    "clientName",
    "clientEmail",
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
      this.billings = data;
      this.dataSource = new MatTableDataSource(this.billings);

      this.dataSource.sort = this.sort;
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
