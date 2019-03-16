import { Component, OnInit, Inject } from "@angular/core";
import { AppointmentType } from "src/app/models/AppointmentType";
import { Client } from "src/app/models/Client";
import { Therapist } from "src/app/models/Therapist";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ClientService } from "src/app/services/client.service";
import { TherapistService } from "src/app/services/therapist.service";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { AppointmentTypeService } from "src/app/services/appointment-type.service";
import { AppointmentInterface } from "../interfaces/appointment";
import { AppointmentsService } from "src/app/services/appointments.service";
import { NgxMaterialTimepickerTheme } from "ngx-material-timepicker";
import * as jwt_decode from "jwt-decode";

@Component({
  selector: "app-appointment-operations",
  templateUrl: "./appointment-operations.component.html",
  styleUrls: ["./appointment-operations.component.scss"]
})
export class AppointmentOperationsComponent implements OnInit {
  clients: Client[];
  therapists: Therapist[] = [];
  appointmentTypes: AppointmentType[];

  appointmentForm: FormGroup;

  constructor(
    private clientService: ClientService,
    private therapistService: TherapistService,
    private appointmentTypeService: AppointmentTypeService,
    private formBuilder: FormBuilder,
    private appointmentService: AppointmentsService,
    public dialogRef: MatDialogRef<AppointmentOperationsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AppointmentInterface
  ) {}

  ngOnInit() {
    this.getAppointmentsType();
    this.getClients();
    this.getTherapists();

    if (!this.data) {
      this.data = { editing: false } as AppointmentInterface;
    }

    if (this.data.editing) {
      this.appointmentForm = this.formBuilder.group({
        id: this.data.id,
        appointmentTypeId: {
          value: this.data.appointmentTypeId,
          disabled: true
        },
        clientId: { value: this.data.clientId, disabled: true },
        therapistId: { value: this.data.therapistId, disabled: true },
        appointmentDate: this.data.appointmentDate,
        appointmentTime: this.data.appointmentTime
      });
      return;
    }

    this.appointmentForm = this.formBuilder.group({
      appointmentTypeId: [null, Validators.required],
      clientId: [null, Validators.required],
      therapistId: [null, Validators.required],
      appointmentDate: [null, Validators.required],
      appointmentTime: [null, Validators.required]
    });
  }

  websiteTheme: NgxMaterialTimepickerTheme = {
    container: {
      bodyBackgroundColor: "#ffffff",
      buttonColor: "#EC407A"
    },
    dial: {
      dialBackgroundColor: "#EC407A"
    },
    clockFace: {
      clockFaceBackgroundColor: "#ffffff",
      clockHandColor: "#EC407A",
      clockFaceTimeInactiveColor: "#686868"
    }
  };

  getAppointmentsType() {
    this.appointmentTypeService.get().subscribe(data => {
      this.appointmentTypes = data;
    });
  }

  getClients() {
    this.clientService.get().subscribe(data => {
      this.clients = data;
    });
  }

  getTherapists() {
    let local = JSON.parse(localStorage.getItem("currentUser"));
    let token = local ? jwt_decode(local.token) : null;

    this.therapistService.get().subscribe(data => {
      this.therapists.push(data.find(x => x.id == token.nameid));
    });
  }

  selectedClient(event) {
    this.appointmentForm.patchValue({
      clientId: event
    });
  }

  selectedTherapist(event) {
    this.appointmentForm.patchValue({
      therapistId: event
    });
  }

  selectedAppointmentType(event) {
    this.appointmentForm.patchValue({
      appointmentTypeId: event
    });
  }

  selectedAppointments(event) {
    this.appointmentForm.patchValue({
      appointmentId: event
    });
  }

  onSave() {
    if (this.data.editing) {
      this.appointmentService
        .edit(this.appointmentForm.value.id, this.appointmentForm.value)
        .subscribe(
          () => {
            this.dialogRef.close();
          },
          error => {
            console.log(error);
          }
        );
      return;
    }

    this.appointmentService.save(this.appointmentForm.value).subscribe(
      () => {
        this.dialogRef.close();
      },
      error => {
        console.log(error);
      }
    );
  }

  onClose() {
    this.dialogRef.close();
  }
}
