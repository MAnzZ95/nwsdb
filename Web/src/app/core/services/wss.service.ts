import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { Wss } from '../models/wsses/wss';

@Injectable({
  providedIn: 'root'
})
export class WssService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/wsses`;

  getWssDetails(): Observable<Wss[]> {
    return this.httpClient.get<Wss[]>(`${this.apiUrl}`);
  }

  getWssDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<Wss[]>{
    return this.httpClient.get<Wss[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getWssDetailsById(wssId: string): Observable<Wss>{
    return this.httpClient.get<Wss>(`${this.apiUrl}/${wssId}`);
  }

  createWss(wss: Wss): Observable<Wss>{
    return this.httpClient.post<Wss>(`${this.apiUrl}`,wss);
  }

  modifyWss(wss: Wss): Observable<Wss>{
    return this.httpClient.put<Wss>(`${this.apiUrl}`,wss);
  }

  getWsssCount(): Observable<number>{
    return this.httpClient.get<number>(`${this.apiUrl}/count`);
  }

  removeWss(wssId: string): Observable<Wss> {
    return this.httpClient.delete<Wss>(`${this.apiUrl}/${wssId}`);
  }

  getWssDetailsByRmoId(rmoId: string): Observable<Wss[]>{
    return this.httpClient.get<Wss[]>(`${this.apiUrl}/${rmoId}`);
  }
}
