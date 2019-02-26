import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrl } from "../authentication/services/apiUrl";
import { AppointmentType } from "../models/AppointmentType";

@Injectable({
  providedIn: "root"
})
export class AppointmentTypeService {
  private readonly apiUrl: ApiUrl;

  constructor(private http: HttpClient) {
    this.apiUrl = new ApiUrl();
  }

  get() {
    return this.http.get<AppointmentType[]>(
      this.apiUrl.endpoint + "/appointmentType"
    );
  }
}
