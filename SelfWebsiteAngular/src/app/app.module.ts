import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { MaterialModule } from './material.module';
import { SelfAuthModule } from './auth/self-auth.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ResumeComponent } from './resume/resume/resume.component';
import { ResumeAdminComponent } from './resume/resume-admin/resume-admin.component';
import { MenuComponent } from './menu/menu.component';
import { InitialComponent } from './initial/initial.component';
import { ResumeService } from './services/resume.service';
import { SectionComponent } from './resume/section/section.component';
import { environment } from 'src/environments/environment';
import { SectionAdminComponent } from './resume/section-admin/section-admin.component';
import { ResumeChildrenInteractionService } from './services/resume-children-interaction.service';
import { MatNativeDateModule } from '@angular/material/core';
import { LinkComponent } from './resume/link/link.component';
import { LinkAdminComponent } from './resume/link-admin/link-admin.component';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faGithub, faLinkedin } from '@fortawesome/free-brands-svg-icons';
import { faEnvelope } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'

export function tokenGetter() {
  return localStorage.getItem(environment.accessTokenName);
}

@NgModule({
  declarations: [
    ResumeComponent,
    ResumeAdminComponent,
    MenuComponent,
    InitialComponent,
    SectionComponent,
    SectionAdminComponent,
    LinkComponent,
    LinkAdminComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    SelfAuthModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MatNativeDateModule,
    FontAwesomeModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7131"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    ResumeService,
    ResumeChildrenInteractionService
  ],
  bootstrap: [InitialComponent]
})
export class AppModule {
  constructor(FaIconLibrary: FaIconLibrary) {
    FaIconLibrary.addIcons(faEnvelope, faLinkedin, faGithub);
  }
}
