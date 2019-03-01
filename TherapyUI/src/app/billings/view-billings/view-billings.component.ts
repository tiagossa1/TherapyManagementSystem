import { Component, OnInit, ViewChild } from "@angular/core";
import { BillingService } from "../services/billing.service";
import { Billing } from "src/app/models/Billing";
import { MatTableDataSource, MatSort, MatDialog } from "@angular/material";
import { BillingOperationsComponent } from "../billing-operations/billing-operations.component";
import Swal from "sweetalert2";

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
    "price",
    "discount",
    "delete",
    "edit"
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

  openDialog(value) {
    if (value) {
      const dialogRef = this.dialog.open(BillingOperationsComponent, {
        width: "250px",
        data: {
          id: value.id,
          appointmentId: value.appointment.id,
          clientId: value.appointment.client.id,
          therapistId: value.appointment.therapist.id,
          price: value.price,
          editing: true,
          discount: value.discount
        }
      });

      dialogRef.afterClosed().subscribe(() => {
        this.getBillings();
      });
      return;
    }

    const dialogRef = this.dialog.open(BillingOperationsComponent, {
      width: "550px",
      height: "500px"
    });

    dialogRef.afterClosed().subscribe(() => {
      this.getBillings();
    });
  }

  onDelete(id: string) {
    Swal({
      title: "Deleting warning",
      text: "Are you sure you want to delete this billing?",
      type: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes"
    }).then(result => {
      if (result.value) {
        this.billingService.delete(id).subscribe(
          () => {
            Swal("Deleted!", "Billing deleted!", "success");
            this.getBillings();
          },
          error => {
            Swal("Error", error.message, "error");
          }
        );
      }
    });
  }
}
