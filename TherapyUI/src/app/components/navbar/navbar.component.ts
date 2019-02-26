import { Component, OnInit, SimpleChanges } from "@angular/core";
import { AuthService } from "src/app/authentication/services/auth.service";
import { Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";

@Component({
  selector: "app-navbar",
  templateUrl: "./navbar.component.html",
  styleUrls: ["./navbar.component.scss"]
})
export class NavbarComponent implements OnInit {
  constructor(
    public authService: AuthService,
    private router: Router,
    private translate: TranslateService
  ) {}

  username: string;

  ngOnInit() {
    this.authService.username.subscribe(data => {
      this.username = data;
    });

    this.username = this.authService.getUsername();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["login"]);
  }

  useLanguage(language: string) {
    this.translate.use(language);
  }
}
