import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { MDBBootstrapModule } from "angular-bootstrap-md";

import { AppComponent } from "./app.component";
import { NavbarComponent } from "./navbar/navbar.component";
import { IndexComponent } from './index/index.component';
import { AuthorizationModule } from "./authorization/authorization.module";
import { AppRoutes } from "./app.routing";

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      IndexComponent
   ],
   imports: [
      BrowserModule,
      AuthorizationModule,
      AppRoutes,
      MDBBootstrapModule.forRoot()
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule {}
