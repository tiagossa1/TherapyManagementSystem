import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrl } from "../authentication/services/apiUrl";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class AppointmentsService {
  private readonly apiUrl: ApiUrl;
  constructor(private http: HttpClient) {
    this.apiUrl = new ApiUrl();
  }

  get() {
    return this.http
      .get(this.apiUrl.endpoint + "/appointment");
  }
}
