import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ResumeComponent } from './resume/resume/resume.component';
import { AuthInitialComponent } from './auth/auth-initial/auth-initial.component';
import { LoginComponent } from './auth/login/login.component';
import { ResumeAdminComponent } from './resume/resume-admin/resume-admin.component';
import { AuthGuard } from './auth/auth-guard';
import { PixivLinksComponent } from './pixiv-links/pixiv-links/pixiv-links.component';

const routes: Routes = [
  //{ path: '', redirectTo: 'resume', pathMatch: 'full' },
  { path: 'resume', component: ResumeComponent },
  { path: 'resume-admin', component: ResumeAdminComponent, canActivate: [AuthGuard] },
  {
    path: 'auth', component: AuthInitialComponent, children: [
      { path: 'login', component: LoginComponent }]
  },
  { path: 'pixiv-links', component: PixivLinksComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
