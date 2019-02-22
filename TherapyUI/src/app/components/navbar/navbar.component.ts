import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/authentication/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  userLogged() {
    return this.authService.isAuthentication();
  }

  ngOnInit() {
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["login"]);
  }

}
