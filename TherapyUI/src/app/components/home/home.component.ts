import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/authentication/services/auth.service";
import { Router } from "@angular/router";
import * as jwt_decode from "jwt-decode";
import { AppointmentsService } from "src/app/services/appointments.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"]
})
export class HomeComponent implements OnInit {
  private local;
  private token;
  appointments: any;

  constructor(private appointmentsService: AppointmentsService) {}

  ngOnInit() {
    if (localStorage.getItem("currentUser")) {
      this.local = JSON.parse(localStorage.getItem("currentUser"));
    }

    this.token = this.local ? jwt_decode(this.local.token) : null;
    console.log(this.token);

    this.getAppointments();

    // if(!this.authService.isAuthentication()) {
    //   this.router.navigate(["index"]);
    // }
  }

  getAppointments() {
    this.appointmentsService.get().subscribe((data) => this.appointments = data);
  }
}
