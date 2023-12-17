import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { DsDivision } from '../models/ds-divisions/ds-divition';
import { LayoutModule } from '@angular/cdk/layout';

@Injectable({
  providedIn: 'root'
})
export class DsDivisionService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/lands`;

  getDSDivisionDetails(): Observable<DsDivision> {
    return this.httpClient.get<DsDivision>(`${this.apiUrl}`);
  }

  getDSDivisionDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<DsDivision[]>{
    return this.httpClient.get<DsDivision[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getDSDivisionDetailsById(dsDivisionId: string): Observable<DsDivision>{
    return this.httpClient.get<DsDivision>(`${this.apiUrl}/${dsDivisionId}`);
  }

  createDSDivision(dsDivision: DsDivision): Observable<DsDivision>{
    return this.httpClient.post<DsDivision>(`${this.apiUrl}`,dsDivision);
  }

  modifyDSDivision(dsDivision: DsDivision): Observable<DsDivision>{
    return this.httpClient.put<DsDivision>(`${this.apiUrl}`,dsDivision);
  }

  getDSDivisionsCount(): Observable<number>{
    return this.httpClient.get<number>(`${this.apiUrl}/count`);
  }

  removeDSDivision(dsDivisionId: string): Observable<DsDivision> {
    return this.httpClient.delete<DsDivision>(`${this.apiUrl}/${dsDivisionId}`);
  }
}
