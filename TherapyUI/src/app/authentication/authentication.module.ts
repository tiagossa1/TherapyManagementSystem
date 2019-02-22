import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AuthenticationComponent } from "./authentication.component";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { AuthenticationRoutes } from "./authentication.routing";

@NgModule({
  imports: [CommonModule, AuthenticationRoutes],
  declarations: [AuthenticationComponent, LoginComponent, RegisterComponent],
  exports: [LoginComponent, RegisterComponent]
})
export class AuthenticationModule {}
