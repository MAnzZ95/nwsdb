import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { Rsc } from '../models/rscs/rsc';

@Injectable({
  providedIn: 'root'
})
export class RscService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/rscs`;

  getRscDetails(): Observable<Rsc> {
    return this.httpClient.get<Rsc>(`${this.apiUrl}`);
  }

  getRscDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<Rsc[]>{
    return this.httpClient.get<Rsc[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getRscDetailsById(rscId: string): Observable<Rsc>{
    return this.httpClient.get<Rsc>(`${this.apiUrl}/${rscId}`);
  }

  createRsc(rsc: Rsc): Observable<Rsc>{
    return this.httpClient.post<Rsc>(`${this.apiUrl}`,rsc);
  }

  modifyRsc(rsc: Rsc): Observable<Rsc>{
    return this.httpClient.put<Rsc>(`${this.apiUrl}`,rsc);
  }

  getRscsCount(): Observable<number>{
    return this.httpClient.get<number>(`${this.apiUrl}/count`);
  }

  removeRsc(rscId: string): Observable<Rsc> {
    return this.httpClient.delete<Rsc>(`${this.apiUrl}/${rscId}`);
  }
}
