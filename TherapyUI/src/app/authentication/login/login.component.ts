import { Component, OnInit } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { AlertService } from "ngx-alerts";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {
  loginForm: any;
  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private translate: TranslateService,
    private alertService: AlertService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ["", Validators.required],
      password: ["", Validators.required]
    });
  }

  authenticate() {
    this.authService
      .authenticate(this.loginForm.value)
      .pipe()
      .subscribe(
        data => {
          this.router.navigate(["/"]);
        },
        error => {
          this.translate.get("logged.error").subscribe((res: string) => {
            this.alertService.danger(res);
          });
        }
      );
  }
}
