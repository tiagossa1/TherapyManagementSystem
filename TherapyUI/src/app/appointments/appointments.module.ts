import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AppointmentsComponent } from "./appointments.component";
import { SweetAlert2Module } from "@toverux/ngx-sweetalert2";
import { AppointmentsRoutes } from "./appointments.routing";

import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import { MatSortModule } from "@angular/material/sort";
import { MatDialogModule } from "@angular/material/dialog";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { AppointmentOperationsComponent } from "./appointment-operations/appointment-operations.component";
import { ViewAppointmentsComponent } from "./view-appointments/view-appointments.component";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { HttpLoaderFactory } from "../app.module";
import {
  MatIconModule,
  MatTooltipModule,
  MatNativeDateModule,
  DateAdapter,
  MAT_DATE_LOCALE
} from "@angular/material";
import { HttpClient } from "@angular/common/http";
import { ReactiveFormsModule } from "@angular/forms";
import { NgxMaterialTimepickerModule } from "ngx-material-timepicker";
import { DateFormat } from "../DateFormat";
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
    MatIconModule,
    MatTooltipModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    SweetAlert2Module.forRoot(),
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgxMaterialTimepickerModule.forRoot()
  ],
  declarations: [
    AppointmentsComponent,
    AppointmentOperationsComponent,
    ViewAppointmentsComponent
  ],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: "pt-PT" }],
  entryComponents: [AppointmentOperationsComponent],
  exports: [AppointmentOperationsComponent, ViewAppointmentsComponent]
})
export class AppointmentsModule {}
