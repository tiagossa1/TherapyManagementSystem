import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ApiUrl } from "src/app/authentication/services/apiUrl";
import { Billing } from "src/app/models/Billing";
import { catchError, map } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class BillingService {
  private readonly apiUrl: ApiUrl;
  constructor(private http: HttpClient) {
    this.apiUrl = new ApiUrl();
  }

  get() {
    return this.http.get<Billing[]>(this.apiUrl.endpoint + "/billing");
  }

  save(body: any) {
    return this.http.post(this.apiUrl.endpoint + "/billing", body);
  }
}
