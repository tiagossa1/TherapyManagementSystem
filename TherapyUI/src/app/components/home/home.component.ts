import { Component, OnInit } from "@angular/core";
import * as jwt_decode from "jwt-decode";
import { AppointmentsService } from "src/app/services/appointments.service";
import { Appointment } from "src/app/models/Appointment";
import { TranslateService } from "@ngx-translate/core";
import { AuthService } from "src/app/authentication/services/auth.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"]
})
export class HomeComponent implements OnInit {
  private local;
  token;
  appointments: Appointment[] = [];
  displayedColumns: string[] = [
    "client",
    "clientMobileNumber",
    "clientObservations",
    "appointmentType"
  ];

  constructor(
    private appointmentsService: AppointmentsService,
    private authService: AuthService,
    private translate: TranslateService
  ) {}

  username: string = this.authService.getUsername();

  ngOnInit() {
    if (localStorage.getItem("currentUser")) {
      this.local = JSON.parse(localStorage.getItem("currentUser"));
    }

    this.token = this.local ? jwt_decode(this.local.token) : null;
    this.getAppointments();

    // if(!this.authService.isAuthenticated()) {
    //   this.router.navigate(["index"]);
    // }
  }

  getAppointments() {
    let day: Date = new Date();
    this.appointmentsService.get().subscribe((data: Appointment[]) => {
      data.forEach(element => {
        let appointmentDate = element.appointmentDate.toString().split("/");

        if (
          element.therapist.id == this.token.nameid &&
          appointmentDate[0] == day.getDate().toString() &&
          appointmentDate[1] == "0" + (day.getMonth() + 1)
        ) {
          this.appointments.push(element);
        }
      });
    });
  }
}
