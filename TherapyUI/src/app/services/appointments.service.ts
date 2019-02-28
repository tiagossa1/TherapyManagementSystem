import { Injectable } from "@angular/core";
import { ApiUrl } from "../authentication/services/apiUrl";
import { HttpClient } from "@angular/common/http";
import { Appointment } from "../models/Appointment";

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

  save(body: any) {
    return this.http.post(this.apiUrl.endpoint + "/appointment", body);
  }

  delete(id: string) {
    return this.http.delete(this.apiUrl.endpoint + "/appointment/" + id, {
      responseType: "text"
    });
  }

  edit(id: string, body: any) {
    return this.http.put(`${this.apiUrl.endpoint}/appointment/${id}`, body);
  }
}
