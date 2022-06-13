import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SelfAuthService } from '../services/self-auth.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private selfAuthService: SelfAuthService, private router: Router, private jwtHelper: JwtHelperService) { }

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.selfAuthService.IsAuthenticated()) {
            return true;
        }

        const isRefreshSuccess = await this.selfAuthService.TryRefreshToken();
        if (!isRefreshSuccess) {
            this.router.navigate(["auth/login"]);
        }

        return isRefreshSuccess;
    }
}