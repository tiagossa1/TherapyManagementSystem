import { Component, OnInit, ViewChild } from "@angular/core";
import { MatSort, MatTableDataSource, MatDialog } from "@angular/material";
import { AppointmentsService } from "src/app/services/appointments.service";
import { AppointmentOperationsComponent } from "../appointment-operations/appointment-operations.component";
import { Appointment } from "src/app/models/Appointment";
import Swal from "sweetalert2";
import * as jwt_decode from "jwt-decode";

@Component({
  selector: "app-view-appointments",
  templateUrl: "./view-appointments.component.html",
  styleUrls: ["./view-appointments.component.scss"]
})
export class ViewAppointmentsComponent implements OnInit {
  appointments;
  token;
  local;
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
    if (localStorage.getItem("currentUser")) {
      this.local = JSON.parse(localStorage.getItem("currentUser"));
    }

    this.token = this.local ? jwt_decode(this.local.token) : null;
    this.getAppointments();
  }

  getAppointments() {
    this.appointmentsService.get().subscribe(data => {
      let array: Appointment[] = [];
      data.forEach(element => {
        if (element.therapist.id == this.token.nameid) {
          array.push(element);
        }
      });

      this.appointments = new MatTableDataSource(array);
      this.appointments.sort = this.sort;
    });
  }

  openDialog(value) {
    if (value) {
      const dialogRef = this.dialog.open(AppointmentOperationsComponent, {
        width: "250px",
        data: {
          id: value.id,
          appointmentTypeId: value.appointmentType.id,
          clientId: value.client.id,
          therapistId: value.therapist.id,
          appointmentDate: value.appointmentDate,
          appointmentTime: value.appointmentTime,
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
    if (navigator.language.toLowerCase() != "pt-pt") {
      Swal({
        title: "Deleting warning",
        text: "Are you sure you want to delete this appointment?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
      }).then(result => {
        if (result.value) {
          this.appointmentsService.delete(id).subscribe(
            () => {
              Swal("Deleted!", "Appoointment deleted!", "success");
              this.getAppointments();
            },
            error => {
              Swal("Error", error.message, "error");
            }
          );
        }
      });
    } else {
      Swal({
        title: "Aviso",
        text: "Pertende eliminar esta consulta?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
      }).then(result => {
        if (result.value) {
          this.appointmentsService.delete(id).subscribe(
            () => {
              Swal("Apagada!", "Consulta apagada!", "success");
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

  applyFilter(filterValue: string) {
    this.appointments.filter = filterValue.trim().toLowerCase();
  }
}
