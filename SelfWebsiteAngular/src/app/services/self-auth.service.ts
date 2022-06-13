import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ITokenModel } from '../models/auth/ITokenModel';
import { environment } from 'src/environments/environment';
import { ILogin } from '../models/auth/ILogin';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class SelfAuthService {
  private apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  public Login(credentials: ILogin) {
    return this.http.post<ITokenModel>(`${this.apiUrl}/auth/Login`, credentials);
  }

  public Logout() {
    localStorage.removeItem(environment.accessTokenName);
    localStorage.removeItem(environment.refreshTokenName);
  }

  public IsAuthenticated() {
    const token = localStorage.getItem(environment.accessTokenName);
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    return false;
  }

  public IsRefreshTokenPresent() {
    const token = localStorage.getItem(environment.refreshTokenName);
    return token ? true : false;
  }

  public async TryRefreshToken(): Promise<boolean> {
    const accessToken = localStorage.getItem(environment.accessTokenName);
    const refreshToken = localStorage.getItem(environment.refreshTokenName);
    if (!accessToken || !refreshToken) {
      return false;
    }

    const credentials = JSON.stringify({ accessToken: accessToken, refreshToken: refreshToken });
    let isRefreshSuccess: boolean;
    const refreshed = await new Promise<ITokenModel>((resolve, reject) => {
      this.http.post<ITokenModel>(`${this.apiUrl}/Auth/Refresh`, credentials
        , {
          headers: new HttpHeaders({
            "Content-Type": "application/json"
          })
        }
      ).subscribe({
        next: (res: ITokenModel) => resolve(res),
        error: (_) => { reject; isRefreshSuccess = false; }
      });
    });

    localStorage.setItem(environment.accessTokenName, refreshed.accessToken);
    localStorage.setItem(environment.refreshTokenName, refreshed.refreshToken);
    isRefreshSuccess = true;

    return isRefreshSuccess;
  }
}
