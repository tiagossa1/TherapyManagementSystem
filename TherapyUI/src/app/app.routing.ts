import { Routes, RouterModule } from "@angular/router";
import { ModuleWithProviders } from "@angular/compiler/src/core";
import { IndexComponent } from "./index/index.component";

const routes: Routes = [
  { path: "index", component: IndexComponent },
  { path: "", redirectTo: "index", pathMatch: "full" },
  {
    path: "login",
    loadChildren: "./authorization/authorization.module#AuthorizationModule"
  }
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
