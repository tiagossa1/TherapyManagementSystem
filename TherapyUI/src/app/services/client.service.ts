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

  delete(id: string) {
    return this.http.delete(this.apiUrl.endpoint + "/client/" + id, {
      responseType: "text"
    });
  }

  edit(id: string, body: any) {
    return this.http.put(this.apiUrl.endpoint + "/client/" + id, body);
  }

  save(body: any) {
    return this.http.post(this.apiUrl.endpoint + "/client", body);
  }
}
