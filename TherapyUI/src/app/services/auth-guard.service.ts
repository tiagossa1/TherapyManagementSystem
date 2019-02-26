import { Injectable } from "@angular/core";
import { Router, CanLoad, CanActivate } from "@angular/router";
import { AuthService } from "../authentication/services/auth.service";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class AuthGuardService implements CanActivate {
  constructor(public authService: AuthService, public router: Router) {}

  canActivate(): boolean {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(["login"]);
      return false;
    }

    return true;
  }
}
