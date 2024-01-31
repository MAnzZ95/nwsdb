import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { KeycloakService } from 'keycloak-angular';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: []
})
export class AppHeaderComponent {

  constructor(
    //private route: ActivatedRoute,
    private router: Router,
    private keycloakService: KeycloakService
  ) {}

  signOut() {
    this.keycloakService.logout();
  }
}
