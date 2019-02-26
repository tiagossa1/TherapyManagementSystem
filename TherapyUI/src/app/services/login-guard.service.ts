import { Injectable } from "@angular/core";
import { Router, CanActivate } from "@angular/router";
import { AuthService } from "../authentication/services/auth.service";

@Injectable({
  providedIn: "root"
})
export class LoginGuardService implements CanActivate {
  constructor(private authService: AuthService, public router: Router) {}

  canActivate(): boolean {
    if (this.authService.isAuthenticated()) {
      console.log(this.authService.isAuthenticated());
      this.router.navigate(["index"]);
      return false;
    }

    console.log(this.authService.isAuthenticated());

    return true;
  }
}
