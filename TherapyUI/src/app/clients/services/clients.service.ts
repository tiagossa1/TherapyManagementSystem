import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrl } from "src/app/authentication/services/apiUrl";
import { Client } from "src/app/models/Client";

@Injectable({
  providedIn: "root"
})
export class ClientsService {
  private readonly apiUrl: ApiUrl;
  constructor(private http: HttpClient) {
    this.apiUrl = new ApiUrl();
  }

  get() {
    return this.http.get<Client[]>(`${this.apiUrl.endpoint}/client/`);
  }

  save(body: any) {
    return this.http.post(`${this.apiUrl.endpoint}/client`, body);
  }
}
