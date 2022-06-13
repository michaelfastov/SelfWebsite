import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IResume } from '../models/resume/IResume';

@Injectable({
  providedIn: 'root'
})
export class ResumeService {
  private apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  GetMainResume() {
    //this.authService.loadToken();
    // const headers = new HttpHeaders().set(
    //   'Authorization',
    //   `Bearer ${this.authService.authToken}`
    // );
    //formValue.nor_id = this.authService.getNorId();
    return this.http.get<IResume>(`${this.apiUrl}/resume/GetMainResume`);
  }

  SaveResume(resume: IResume) {
    return this.http.post<IResume>(`${this.apiUrl}/resume/admin/CreateOrUpdateResume`, resume,
      {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }
}
