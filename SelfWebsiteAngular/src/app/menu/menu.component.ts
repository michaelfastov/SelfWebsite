import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { SelfAuthService } from '../services/self-auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  constructor(private selfAuthService: SelfAuthService, private route: ActivatedRoute, private router: RouterModule) { }

  ngOnInit(): void {
  }

  public Logout() {
    this.selfAuthService.Logout();
  }

  public IsRefreshTokenPresent(): boolean {
    return this.selfAuthService.IsRefreshTokenPresent();
  }
}
