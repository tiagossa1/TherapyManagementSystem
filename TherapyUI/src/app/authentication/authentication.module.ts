import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AuthenticationComponent } from "./authentication.component";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { AuthenticationRoutes } from "./authentication.routing";

import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";

import { HttpClientModule } from "@angular/common/http";
import { ReactiveFormsModule } from "@angular/forms";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";

@NgModule({
  imports: [
    CommonModule,
    AuthenticationRoutes,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    HttpClientModule,
    ReactiveFormsModule,
    TranslateModule,
    FormsModule
  ],
  declarations: [AuthenticationComponent, LoginComponent, RegisterComponent],
  exports: [LoginComponent, RegisterComponent]
})
export class AuthenticationModule {}
