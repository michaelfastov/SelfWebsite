import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RedisCacheService {
  private apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  GetCachedLinksForUser() {
    return this.http.get<string[]>(`${this.apiUrl}/PixivLinks/GetCachedLinksByUsername`);
  }

  CachePixivLinkForUser(link: string) {
    debugger
    return this.http.post(`${this.apiUrl}/PixivLinks/CachePixivLinkForUser`, `\"${link}\"`,
      {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      })
    };
}
