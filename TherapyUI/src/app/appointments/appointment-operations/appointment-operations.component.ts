import { Component, OnInit, Inject } from "@angular/core";
import { AppointmentType } from "src/app/models/AppointmentType";
import { Client } from "src/app/models/Client";
import { Therapist } from "src/app/models/Therapist";
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl
} from "@angular/forms";
import { ClientService } from "src/app/services/client.service";
import { TherapistService } from "src/app/services/therapist.service";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { AppointmentTypeService } from "src/app/services/appointment-type.service";
import { AppointmentInterface } from "../interfaces/appointment";
import { AppointmentsService } from "src/app/services/appointments.service";
import { NgxMaterialTimepickerTheme } from "ngx-material-timepicker";
import { DatePipe } from "@angular/common";

@Component({
  selector: "app-appointment-operations",
  templateUrl: "./appointment-operations.component.html",
  styleUrls: ["./appointment-operations.component.scss"]
})
export class AppointmentOperationsComponent implements OnInit {
  appointmentDate: FormControl = new FormControl("", Validators.required);
  appointmentTime: FormControl = new FormControl("", Validators.required);

  clients: Client[];
  therapists: Therapist[];
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
        appointmentTypeId: this.data.appointmentTypeId,
        clientId: this.data.clientId,
        therapistId: this.data.therapistId,
        appointmentDate: [null]
      });

      let date = this.data.appointmentDate.toString().split("/");
      let time = date[2].split(":");
      let year = time[0].split(" ");

      let timeTransformed: string = year[1] + ":" + time[1] + ":" + time[2]; // hh:mm:ss

      this.appointmentDate.setValue(
        new Date(Number(year[0]), Number(date[1]) - 1, Number(date[0]))
      );
      this.appointmentTime.setValue(timeTransformed);
      return;
    }

    this.appointmentForm = this.formBuilder.group({
      appointmentTypeId: ["", Validators.required],
      clientId: ["", Validators.required],
      therapistId: ["", Validators.required],
      appointmentDate: [new Date()]
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
    this.therapistService.get().subscribe(data => {
      this.therapists = data;
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

  onSave() { debugger
    let datePipe = new DatePipe("en-US");
    let dateTransformed: string = datePipe.transform(
      this.appointmentDate.value,
      "yyyy/MM/dd"
    );
    // 'dd/MM/yyyy'

    if (this.appointmentDate.valid && this.appointmentTime.valid) {
      this.appointmentForm.patchValue({
        appointmentDate: dateTransformed + " " + this.appointmentTime.value
      });
    }

    console.log(this.appointmentForm.value);

    if (this.data.editing) {
      console.log(this.appointmentForm.value);
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
