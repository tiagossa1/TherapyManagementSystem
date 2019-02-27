import { Component, OnInit, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { Appointment } from "src/app/models/Appointment";
import { Therapist } from "src/app/models/Therapist";
import { Client } from "src/app/models/Client";
import { FormGroup, FormBuilder } from "@angular/forms";
import { ClientService } from "src/app/services/client.service";
import { TherapistService } from "src/app/services/therapist.service";
import { AppointmentsService } from "src/app/services/appointments.service";
import { BillingService } from "../services/billing.service";
import { BillingInterface } from "../interfaces/billing";

@Component({
  selector: "app-edit-billing",
  templateUrl: "./edit-billing.component.html",
  styleUrls: ["./edit-billing.component.css"]
})
export class EditBillingComponent implements OnInit {
  clients: Client[];
  therapists: Therapist[];
  appointments: Appointment[] = [];

  billingForm: FormGroup;
  constructor(
    private clientService: ClientService,
    private therapistService: TherapistService,
    private appointmentService: AppointmentsService,
    private billingService: BillingService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<EditBillingComponent>,
    @Inject(MAT_DIALOG_DATA) public data: BillingInterface
  ) {}

  ngOnInit() {
    this.getClients();
    this.getTherapists();
    this.getAppointments();

    this.billingForm = this.formBuilder.group({
      id: this.data.id,
      appointmentId: this.data.appointmentId,
      clientId: this.data.clientId,
      therapistId: this.data.therapistId,
      price: this.data.price
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

  getAppointments() {
    this.appointmentService.get().subscribe(data => {
      data.forEach(value => {
        if (
          value.client.id == this.data.clientId &&
          value.therapist.id == this.data.therapistId
        ) {
          this.appointments.push(value);
        }
      });
    });
  }

  selectedClient(event) {
    this.billingForm.patchValue({
      clientId: event
    });

    if (this.billingForm.value.clientId && this.billingForm.value.therapistId) {
      this.getAppointments();
    }
  }

  selectedTherapist(event) {
    this.billingForm.patchValue({
      therapistId: event
    });

    if (this.billingForm.value.clientId && this.billingForm.value.therapistId) {
      this.getAppointments();
    }
  }

  selectedAppointments(event) {
    this.billingForm.patchValue({
      appointmentId: event
    });
  }

  onClose() {
    this.dialogRef.close();
  }

  onEdit() {
    debugger
    console.log(this.billingForm.value);
    let form = {
      id: this.billingForm.value.id,
      appointmentId: this.billingForm.value.appointmentId,
      price: this.billingForm.value.price
    }
    this.billingService.edit(this.billingForm.value.id, form).subscribe(
      () => {
        this.dialogRef.close();
      },
      error => {
        console.log(error);
      }
    );
  }
}
