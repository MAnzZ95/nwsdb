import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { Rmo } from '../models/rmos/rmo';

@Injectable({
  providedIn: 'root'
})
export class RmoService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/rmos`;

  getRmoDetails(): Observable<Rmo> {
    return this.httpClient.get<Rmo>(`${this.apiUrl}`);
  }

  getRmoDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<Rmo[]>{
    return this.httpClient.get<Rmo[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getRmoDetailsById(rmoId: string): Observable<Rmo>{
    return this.httpClient.get<Rmo>(`${this.apiUrl}/${rmoId}`);
  }

  createRmo(rmo: Rmo): Observable<Rmo>{
    return this.httpClient.post<Rmo>(`${this.apiUrl}`,rmo);
  }

  modifyRmo(rmo: Rmo): Observable<Rmo>{
    return this.httpClient.put<Rmo>(`${this.apiUrl}`,rmo);
  }

  getRmosCount(): Observable<number>{
    return this.httpClient.get<number>(`${this.apiUrl}/count`);
  }

  removeRmo(rmoId: string): Observable<Rmo> {
    return this.httpClient.delete<Rmo>(`${this.apiUrl}/${rmoId}`);
  }
}
