import { Injectable } from "@angular/core";
import { ApiUrl } from "../authentication/services/apiUrl";
import { HttpClient } from "@angular/common/http";
import { Therapist } from "../models/Therapist";

@Injectable({
  providedIn: "root"
})
export class TherapistService {
  private readonly apiUrl: ApiUrl;
  constructor(private http: HttpClient) {
    this.apiUrl = new ApiUrl();
  }

  get() {
    return this.http.get<Therapist[]>(this.apiUrl.endpoint + "/therapist");
  }
}
