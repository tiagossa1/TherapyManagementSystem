import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrl } from "./apiUrl";
import { Authenticate } from "../models/authenticate";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  private readonly apiUrl: ApiUrl;
  constructor(private http: HttpClient) {
    this.apiUrl = new ApiUrl();
  }

  authenticate(body: Authenticate) {
    return this.http
      .post<any>(this.apiUrl.endpoint + "/therapist/authenticate", body)
      .pipe(
        map(user => {
          if (user && user.token) {
            localStorage.setItem("currentUser", JSON.stringify(user));
          }

          return user;
        })
      );
  }

  logout() {
    localStorage.removeItem("currentUser");
  }

  public isAuthentication(): boolean {
    const local: any = JSON.parse(localStorage.getItem("currentUser"));
    return local && local.token ? true : false;
  }
}
