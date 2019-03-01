import { Component, OnInit, Inject } from "@angular/core";
import { ClientService } from "src/app/services/client.service";
import { Client } from "src/app/models/Client";
import { TherapistService } from "src/app/services/therapist.service";
import { Therapist } from "src/app/models/Therapist";
import { AppointmentsService } from "src/app/services/appointments.service";
import { Appointment } from "src/app/models/Appointment";
import {
  FormBuilder,
  Validators,
  FormGroup,
  FormControl
} from "@angular/forms";
import { BillingService } from "../services/billing.service";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { BillingInterface } from "../interfaces/billing";
import { Discount } from "src/app/models/Discount";

@Component({
  selector: "app-billing-operations",
  templateUrl: "./billing-operations.component.html",
  styleUrls: ["./billing-operations.component.scss"]
})
export class BillingOperationsComponent implements OnInit {
  discounts: Discount[] = [];
  clients: Client[];
  therapists: Therapist[];
  appointments: Appointment[] = [];

  billingForm: FormGroup;
  discount: FormControl = new FormControl("");

  clientIdSelected: string = null;
  therapistIdSelected: string = null;
  originalPrice: number = 0;

  constructor(
    private clientService: ClientService,
    private therapistService: TherapistService,
    private appointmentService: AppointmentsService,
    private billingService: BillingService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<BillingOperationsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: BillingInterface
  ) {}

  ngOnInit() {
    if (this.discounts.length == 0) {
      for (let i = 5; i <= 100; i += 5) {
        this.discounts.push({ discount: i, toView: i.toString() + "%" });
      }
    }

    this.getClients();
    this.getTherapists();

    if (!this.data) {
      this.data = { editing: false } as BillingInterface;
    }

    if (this.data.editing) {
      this.getAppointments();
      this.billingForm = this.formBuilder.group({
        id: this.data.id,
        appointmentId: this.data.appointmentId,
        clientId: this.data.clientId,
        therapistId: this.data.therapistId,
        price: this.data.price
      });
      return;
    }

    this.billingForm = this.formBuilder.group({
      appointmentId: ["", Validators.required],
      clientId: ["", Validators.required],
      therapistId: ["", Validators.required],
      price: [null, Validators.required],
      discount: [false]
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
        if (this.data.editing) {
          if (
            value.client.id == this.data.clientId &&
            value.therapist.id == this.data.therapistId
          ) {
            this.appointments.push(value);
          }
          return;
        }
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

    if (this.data.editing) {
      if (
        this.billingForm.value.clientId &&
        this.billingForm.value.therapistId
      ) {
        this.getAppointments();
      }
      return;
    }
    if (this.therapistIdSelected && this.clientIdSelected) {
      this.getAppointments();
    }
  }

  selectedTherapist(event) {
    this.therapistIdSelected = event;

    if (this.data.editing) {
      if (
        this.billingForm.value.clientId &&
        this.billingForm.value.therapistId
      ) {
        this.getAppointments();
      }
      return;
    }

    if (this.therapistIdSelected && this.clientIdSelected) {
      this.getAppointments();
    }
  }

  selectedAppointments(event) {
    this.billingForm.patchValue({
      appointmentId: event
    });
  }

  selectedDiscount(event) {
    this.originalPrice = this.billingForm.value.price;
    this.billingForm.patchValue({
      price: this.discountPrice(this.originalPrice, event)
    });
  }

  onSave() {
    if (this.data.editing) {
      console.log(this.billingForm.value);
      let form = {
        id: this.billingForm.value.id,
        appointmentId: this.billingForm.value.appointmentId,
        price: this.billingForm.value.price
      };
      this.billingService.edit(this.billingForm.value.id, form).subscribe(
        () => {
          this.dialogRef.close();
        },
        error => {
          console.log(error);
        }
      );
      return;
    }

    if (this.discount.value) {
      this.billingForm.patchValue({
        discount: true
      });
    }

    this.billingService.save(this.billingForm.value).subscribe(
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

  discountPrice(originalPrice: number, discount: number) {
    return originalPrice - (originalPrice * discount) / 100;
  }
}
