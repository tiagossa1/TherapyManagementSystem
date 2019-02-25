import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent
} from "@angular/common/http";
import { AuthService } from "../services/auth.service";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  helper = new JwtHelperService();
  constructor(private authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let currentUser: any = JSON.parse(localStorage.getItem("currentUser"));
    if (
      currentUser &&
      currentUser.token &&
      !this.helper.isTokenExpired(currentUser.token)
    ) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`
        }
      });
    } else {
      localStorage.removeItem("currentUser");
    }

    return next.handle(req);
  }
}
