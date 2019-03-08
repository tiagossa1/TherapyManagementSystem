import { Component, OnInit, Inject } from "@angular/core";
import {
  FormGroup,
  FormBuilder,
  Validators
} from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { ClientInterface } from "../interfaces/client";
import { ClientService } from "src/app/services/client.service";
import { Client } from "src/app/models/Client";
import { CivilStatus } from "../interfaces/civil-status";
import { Gender } from "../interfaces/gender";

@Component({
  selector: "app-clients-operations",
  templateUrl: "./clients-operations.component.html",
  styleUrls: ["./clients-operations.component.scss"]
})
export class ClientsOperationsComponent implements OnInit {
  clients: Client[] = [];
  civilStatus: CivilStatus[] = [];
  genders: Gender[] = [];

  clientForm: FormGroup;
  constructor(
    private clientService: ClientService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<ClientsOperationsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ClientInterface
  ) {}

  ngOnInit() {
    if (navigator.language.toLowerCase() == "pt-pt") {
      this.civilStatus.push(
        { value: "C", viewValue: "Casado" },
        { value: "S", viewValue: "Solteiro" },
        { value: "D", viewValue: "Divorciada" },
        { value: "V", viewValue: "Viúva" }
      );

      this.genders.push(
        { value: "M", viewValue: "Masculino" },
        { value: "F", viewValue: "Feminino" }
      );
    } else {
      this.civilStatus.push(
        { value: "M", viewValue: "Married" },
        { value: "S", viewValue: "Single" },
        { value: "D", viewValue: "Divorcied" },
        { value: "W", viewValue: "Widow" }
      );

      this.genders.push(
        { value: "M", viewValue: "Male" },
        { value: "F", viewValue: "Female" }
      );
    }
    if (!this.data) {
      this.data = { editing: false } as ClientInterface;
    }

    if (this.data.editing) {
      this.getClients();

      this.clientForm = this.formBuilder.group({
        address: this.data.address,
        civilStatus: this.data.civilStatus,
        dateOfBirth: this.data.dateOfBirth,
        nif: this.data.nif,
        occupation: this.data.occupation,
        observations: this.data.observations,
        id: this.data.id,
        name: this.data.name,
        gender: this.data.gender,
        mobileNumber: this.data.mobileNumber,
        email: this.data.email
      });
      return;
    }

    this.clientForm = this.formBuilder.group({
      address: [null, Validators.required],
      civilStatus: [null, Validators.required],
      dateOfBirth: [null, Validators.required],
      nif: [null, Validators.required],
      occupation: [null, Validators.required],
      observations: [null, Validators.required],
      name: [null, Validators.required],
      gender: [null, Validators.required],
      mobileNumber: [null, Validators.required],
      email: [null, Validators.required]
    });
  }

  getClients() {
    this.clientService.get().subscribe(data => {
      this.clients = data;
    });
  }

  selectedCivilStatus(event) {
    this.clientForm.patchValue({
      civilStatus: event
    });
  }

  selectedGender(event) {
    this.clientForm.patchValue({
      gender: event
    });
  }

  onSave() {
    if (this.data.editing) {
      this.clientService
        .edit(this.clientForm.value.id, this.clientForm.value)
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

    this.clientService.save(this.clientForm.value).subscribe(
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
