import { Component, HostListener } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { Title } from "@angular/platform-browser";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent {
  @HostListener("window:onbeforeunload", ["$event"])
  clearLocalStorage(event) {
    localStorage.removeItem("currentUser");
  }
  constructor(private translate: TranslateService, private tile: Title) {
    translate.setDefaultLang("en");
    this.tile.setTitle("Buddha's Therapy");
  }
}
