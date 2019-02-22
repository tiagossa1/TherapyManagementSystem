import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrl } from "./apiUrl";
import { Authenticate } from "../models/authenticate";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  private readonly apiUrl: ApiUrl;
  constructor(private http: HttpClient) {
    this.apiUrl = new ApiUrl();
  }

  authenticate(body: Authenticate) {
    return this.http.post(this.apiUrl.endpoint + "/therapist/authenticate", body);
  }

  public isAuthentication(): boolean {
    const token = localStorage.getItem("userLogged");
    return token ? true : false;
  }
}
