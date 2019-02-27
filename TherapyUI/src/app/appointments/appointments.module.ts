import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentsComponent } from './appointments.component';
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';
import { NewAppointmentComponent } from './new-appointment/new-appointment.component';
import { ViewAppointmentsComponent } from './view-appointments/view-appointments.component';
import { AppointmentsRoutes } from './appointments.routing';

import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import { MatSortModule } from "@angular/material/sort";
import { MatDialogModule } from "@angular/material/dialog";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";

@NgModule({
  imports: [
    CommonModule,
    AppointmentsRoutes,
    MatTableModule,
    MatButtonModule,
    MatSortModule,
    MatDialogModule,
    MatInputModule,
    MatSelectModule,
    SweetAlert2Module.forRoot()
  ],
  declarations: [AppointmentsComponent, NewAppointmentComponent, ViewAppointmentsComponent],
  exports: [NewAppointmentComponent, ViewAppointmentsComponent]
})
export class AppointmentsModule { }
