import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ClientsComponent } from "./clients.component";
import { ClientsOperationsComponent } from "./clients-operations/clients-operations.component";
import { ViewClientsComponent } from "./view-clients/view-clients.component";
import { ClientsRoutes } from "./clients.routing";
import { MatTableModule } from "@angular/material/table";
import { BrowserModule } from "@angular/platform-browser";
import { TranslateModule } from "@ngx-translate/core";
import { MatTooltipModule } from "@angular/material/tooltip";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { MatInputModule } from "@angular/material/input";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatSelectModule } from "@angular/material/select";
import { ReactiveFormsModule } from "@angular/forms";
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    ClientsRoutes,
    MatTableModule,
    MatTooltipModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatDatepickerModule,
    MatSelectModule,
    BrowserModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    TranslateModule.forRoot()
  ],
  declarations: [
    ClientsComponent,
    ClientsOperationsComponent,
    ViewClientsComponent
  ],
  entryComponents: [ClientsOperationsComponent],
  exports: [ClientsOperationsComponent, ViewClientsComponent]
})
export class ClientsModule {}
