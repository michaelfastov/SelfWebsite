import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth-guard';
import { FormsModule } from '@angular/forms';
import { SelfAuthService } from '../services/self-auth.service';
import { AuthInitialComponent } from './auth-initial/auth-initial.component';
import { AppRoutingModule } from '../app-routing.module';
import { MaterialModule } from '../material.module';

@NgModule({
  declarations: [
    LoginComponent,
    AuthInitialComponent
  ],
  providers: [
    AuthGuard,
    SelfAuthService
  ],
  imports: [
    CommonModule,
    FormsModule,
    AppRoutingModule,
    MaterialModule
  ]
})
export class SelfAuthModule { }
