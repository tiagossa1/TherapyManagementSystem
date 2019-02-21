import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AuthorizationComponent } from "./authorization.component";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { AuthorizationRoutes } from "./authorization.routing";

@NgModule({
  imports: [CommonModule, AuthorizationRoutes],
  declarations: [AuthorizationComponent, LoginComponent, RegisterComponent],
  exports: [AuthorizationComponent, LoginComponent, RegisterComponent]
})
export class AuthorizationModule {}
