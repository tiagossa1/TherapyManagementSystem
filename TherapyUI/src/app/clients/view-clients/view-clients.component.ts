import { Component, OnInit, ViewChild } from "@angular/core";
import { MatSort, MatDialog, MatTableDataSource } from "@angular/material";
import { ClientService } from "src/app/services/client.service";
import { Client } from "src/app/models/Client";
import { ClientsOperationsComponent } from "../clients-operations/clients-operations.component";
import Swal from "sweetalert2";
import { AlertService } from "ngx-alerts";
import { TranslateService } from "@ngx-translate/core";

@Component({
  selector: "app-view-clients",
  templateUrl: "./view-clients.component.html",
  styleUrls: ["./view-clients.component.scss"]
})
export class ViewClientsComponent implements OnInit {
  clients;
  language: string = navigator.language.toLowerCase();
  @ViewChild(MatSort) sort: MatSort;
  displayedColumns: string[] = [
    "clientName",
    "clientDateOfBirth",
    "clientGender",
    "clientEmail",
    "clientMobileNumber",
    "clientAddress",
    "clientCivilStatus",
    "clientNif",
    "clientOccupation",
    "clientObservations",
    "delete",
    "edit"
  ];
  constructor(
    private clientService: ClientService,
    public dialog: MatDialog,
    private alertService: AlertService,
    private translate: TranslateService
  ) {}

  ngOnInit() {
    this.getClients();
  }

  getClients() {
    this.clientService.get().subscribe(data => {
      this.clients = new MatTableDataSource(data as Client[]);
      this.clients.sort = this.sort;
    });
  }

  openDialog(value) {
    if (value) {
      const dialogRef = this.dialog.open(ClientsOperationsComponent, {
        width: "250px",
        data: {
          address: value.address,
          civilStatus: value.civilStatus,
          dateOfBirth: value.dateOfBirth,
          email: value.email,
          gender: value.gender,
          id: value.id,
          mobileNumber: value.mobileNumber,
          name: value.name,
          nif: value.nif,
          observations: value.observations,
          occupation: value.occupation,
          editing: true
        }
      });

      dialogRef.afterClosed().subscribe(() => {
        this.getClients();
        this.translate
          .get("generic.operation.success")
          .subscribe((res: string) => {
            this.alertService.success(res);
          });
      });
      return;
    }

    const dialogRef = this.dialog.open(ClientsOperationsComponent, {
      width: "550px",
      height: "850px"
    });

    dialogRef.afterClosed().subscribe(result => {
      this.getClients();
    });
  }

  onDelete(id: string) {
    if (navigator.language.toLowerCase() != "pt-pt") {
      Swal({
        title: "Deleting warning",
        text: "Are you sure you want to delete this client?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
      }).then(result => {
        if (result.value) {
          this.clientService.delete(id).subscribe(
            () => {
              Swal("Deleted!", "Client deleted!", "success");
              this.getClients();
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
        text: "Pertende eliminar este cliente?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
      }).then(result => {
        if (result.value) {
          this.clientService.delete(id).subscribe(
            () => {
              Swal("Apagado!", "Cliente apagado!", "success");
              this.getClients();
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
    this.clients.filter = filterValue.trim().toLowerCase();
  }
}
