import { Component, OnInit, ViewChild } from "@angular/core";
import { MatSort, MatTableDataSource, MatDialog } from "@angular/material";
import { AppointmentsService } from "src/app/services/appointments.service";
import { AppointmentOperationsComponent } from "../appointment-operations/appointment-operations.component";
import { Appointment } from "src/app/models/Appointment";
import Swal from "sweetalert2";

@Component({
  selector: "app-view-appointments",
  templateUrl: "./view-appointments.component.html",
  styleUrls: ["./view-appointments.component.css"]
})
export class ViewAppointmentsComponent implements OnInit {
  appointments;
  @ViewChild(MatSort) sort: MatSort;

  displayedColumns: string[] = [
    "clientName",
    "clientEmail",
    "appointmentDate",
    "appointmentTypeName",
    "therapistName",
    "delete",
    "edit"
  ];
  constructor(
    private appointmentsService: AppointmentsService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.getAppointments();
  }

  getAppointments() {
    this.appointmentsService.get().subscribe(data => {
      this.appointments = new MatTableDataSource(data as Appointment[]);
      this.appointments.sort = this.sort;
    });
  }

  openDialog(value) {
    if (value) {
      console.log(value);
      const dialogRef = this.dialog.open(AppointmentOperationsComponent, {
        width: "250px",
        data: {
          id: value.id,
          appointmentTypeId: value.appointmentType.id,
          clientId: value.client.id,
          therapistId: value.therapist.id,
          appointmentDate: value.appointmentDate,
          editing: true
        }
      });

      dialogRef.afterClosed().subscribe(() => {
        this.getAppointments();
      });
      return;
    }

    const dialogRef = this.dialog.open(AppointmentOperationsComponent, {
      width: "550px",
      height: "500px"
    });

    dialogRef.afterClosed().subscribe(res => {
      this.getAppointments();
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
        this.appointmentsService.delete(id).subscribe(
          () => {
            Swal("Deleted!", "Billing deleted!", "success");
            this.getAppointments();
          },
          error => {
            Swal("Error", error.message, "error");
          }
        );
      }
    });
  }
}
