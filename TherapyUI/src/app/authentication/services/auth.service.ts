import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrl } from "./apiUrl";
import { Authenticate } from "../models/authenticate";
import { map } from "rxjs/operators";
import { AlertService } from "ngx-alerts";
import { TranslateService } from "@ngx-translate/core";
import * as jwt_decode from "jwt-decode";
import { Subject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  private readonly apiUrl: ApiUrl;
  username = new Subject<string>();

  constructor(
    private http: HttpClient,
    private alertService: AlertService,
    private translate: TranslateService
  ) {
    this.apiUrl = new ApiUrl();
  }

  authenticate(body: Authenticate) {
    return this.http
      .post<any>(this.apiUrl.endpoint + "/therapist/authenticate", body)
      .pipe(
        map(user => {
          if (user && user.token) {
            localStorage.setItem("currentUser", JSON.stringify(user));
            this.translate.get("logged.alert").subscribe((res: string) => {
              this.alertService.success(res);
            });

            this.username.next(this.getUsername());
          }

          return user;
        })
      );
  }

  getUsername(): string {
    let local = JSON.parse(localStorage.getItem("currentUser"));
    let token = local ? jwt_decode(local.token) : null;
    return local ? token.unique_name : "";
  }

  logout() {
    localStorage.removeItem("currentUser");
    this.translate.get("logout.alert").subscribe((res: string) => {
      this.alertService.success(res);
    });
  }

  public isAuthenticated(): boolean {
    const local: any = JSON.parse(localStorage.getItem("currentUser"));
    return local && local.token ? true : false;
  }
}
