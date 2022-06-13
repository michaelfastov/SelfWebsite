import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ILogin } from 'src/app/models/auth/ILogin';
import { SelfAuthService } from 'src/app/services/self-auth.service';
import { ITokenModel } from 'src/app/models/auth/ITokenModel';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  isInvalidLogin: boolean = false;
  credentials: ILogin = { username: '', password: '' };

  constructor(private authService: SelfAuthService) { }

  ngOnInit(): void {
  }

  public Login(form: NgForm) {
    if (form.valid) {
      this.authService.Login(this.credentials)
        .subscribe({
          next: (response: ITokenModel) => {
            localStorage.setItem(environment.accessTokenName, response.accessToken);
            localStorage.setItem(environment.refreshTokenName, response.refreshToken);
            this.isInvalidLogin = false;
            console.log(response.accessToken)
            //this.router.navigate(["/"]);
          }
        })
    }
  }
}
