import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrl } from "../authentication/services/apiUrl";
import { Client } from "../models/Client";

@Injectable({
  providedIn: "root"
})
export class ClientService {
  private readonly apiUrl: ApiUrl;
  constructor(private http: HttpClient) {
    this.apiUrl = new ApiUrl();
  }

  get() {
    return this.http.get<Client[]>(this.apiUrl.endpoint + "/client");
  }
}
