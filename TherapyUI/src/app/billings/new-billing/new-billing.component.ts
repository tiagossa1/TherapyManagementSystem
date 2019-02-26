import { Component, OnInit } from "@angular/core";
import { ClientService } from "src/app/services/client.service";
import { Client } from "src/app/models/Client";
import { TherapistService } from "src/app/services/therapist.service";
import { Therapist } from "src/app/models/Therapist";
import { AppointmentsService } from "src/app/services/appointments.service";
import { Appointment } from "src/app/models/Appointment";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { BillingService } from "../services/billing.service";
import { Subject } from "rxjs";
import { MatDialogRef } from "@angular/material";

@Component({
  selector: "app-new-billing",
  templateUrl: "./new-billing.component.html",
  styleUrls: ["./new-billing.component.scss"]
})
export class NewBillingComponent implements OnInit {
  clients: Client[];
  therapists: Therapist[];
  appointments: Appointment[] = [];

  billingForm: FormGroup;

  clientIdSelected: string = null;
  therapistIdSelected: string = null;

  created = new Subject<boolean>();

  constructor(
    private clientService: ClientService,
    private therapistService: TherapistService,
    private appointmentService: AppointmentsService,
    private billingService: BillingService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<NewBillingComponent>
  ) {}

  ngOnInit() {
    this.getClients();
    this.getTherapists();

    this.billingForm = this.formBuilder.group({
      appointmentId: ["", Validators.required],
      price: [null, Validators.required]
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
          value.client.id == this.clientIdSelected &&
          value.therapist.id == this.therapistIdSelected
        ) {
          this.appointments.push(value);
        }
      });
    });
  }

  selectedClient(event) {
    this.clientIdSelected = event;
    this.billingForm.patchValue({
      clientId: event
    });

    if (this.therapistIdSelected && this.clientIdSelected) {
      this.getAppointments();
    }
  }

  selectedTherapist(event) {
    this.therapistIdSelected = event;

    if (this.therapistIdSelected && this.clientIdSelected) {
      this.getAppointments();
    }
  }

  selectedAppointments(event) {
    this.billingForm.patchValue({
      appointmentId: event
    });
  }

  onSave() {
    console.log(this.billingForm.value);
    this.billingService.save(this.billingForm.value).subscribe(
      () => {
        this.created.next(true);
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
