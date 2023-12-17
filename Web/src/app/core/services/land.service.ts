import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { Land } from '../models/lands/land';
import { LayoutModule } from '@angular/cdk/layout';

@Injectable({
  providedIn: 'root'
})
export class LandService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/lands`;

  getLandDetails(): Observable<Land> {
    return this.httpClient.get<Land>(`${this.apiUrl}`);
  }

  getLandDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<Land[]>{
    return this.httpClient.get<Land[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getLandDetailsById(landId: string): Observable<Land>{
    return this.httpClient.get<Land>(`${this.apiUrl}/${landId}`);
  }

  createLand(land: Land): Observable<Land>{
    return this.httpClient.post<Land>(`${this.apiUrl}`,land);
  }

  modifyLand(land: Land): Observable<Land>{
    return this.httpClient.put<Land>(`${this.apiUrl}`,land);
  }

  getLandsCount(): Observable<number>{
    return this.httpClient.get<number>(`${this.apiUrl}/count`);
  }

  removeLand(landId: string): Observable<Land> {
    return this.httpClient.delete<Land>(`${this.apiUrl}/${landId}`);
  }
}
