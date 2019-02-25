import { Injectable } from "@angular/core";
import { ApiUrl } from "../authentication/services/apiUrl";
import { Appointment } from "../models/Appointment";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class AppointmentsService {
  private readonly apiUrl: ApiUrl;
  constructor(private http: HttpClient) {
    this.apiUrl = new ApiUrl();
  }

  get() {
    return this.http.get<Appointment[]>(this.apiUrl.endpoint + "/appointment");
  }
}
