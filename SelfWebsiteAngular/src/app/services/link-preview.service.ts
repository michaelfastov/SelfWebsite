import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { ILinkPreview } from '../models/ILinkPreview';

@Injectable({
  providedIn: 'root'
})
export class LinkPreviewService {

  constructor(private http: HttpClient) { }

  GetLinkPreview(link: string): Observable<any> {
    const api = `https://api.linkpreview.net/?key=${environment.linkPreviewKey}&q=${link}`;
    return this.http.get(api);
  }
}




