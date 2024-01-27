import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { DsDivision } from '../models/ds-divisions/ds-divition';
import { LayoutModule } from '@angular/cdk/layout';
import { GvDivision } from '../models/gv-divisions/gv-division';

@Injectable({
  providedIn: 'root'
})
export class GnDivisionService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/gsdivisions`;

  getGsDivisionDetails(): Observable<DsDivision[]> {
    return this.httpClient.get<DsDivision[]>(`${this.apiUrl}`);
  }

  getGsDivisionDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<GvDivision[]>{
    return this.httpClient.get<GvDivision[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getGsDivisionDetailsById(gvDivisionId: string): Observable<GvDivision>{
    return this.httpClient.get<GvDivision>(`${this.apiUrl}/${gvDivisionId}`);
  }

  createGsDivision(gvDivision: GvDivision): Observable<GvDivision>{
    return this.httpClient.post<GvDivision>(`${this.apiUrl}`,gvDivision);
  }

  modifyGsDivision(gvDivision: GvDivision): Observable<GvDivision>{
    return this.httpClient.put<GvDivision>(`${this.apiUrl}`,gvDivision);
  }

  getGsDivisionsCount(): Observable<number>{
    return this.httpClient.get<number>(`${this.apiUrl}/count`);
  }

  removeGsDivision(gvDivisionId: string): Observable<GvDivision> {
    return this.httpClient.delete<GvDivision>(`${this.apiUrl}/${gvDivisionId}`);
  }

  getGsDivisionDetailsByDsDivisionId(dsDivisionId: string): Observable<GvDivision[]>{
    return this.httpClient.get<GvDivision[]>(`${this.apiUrl}/${dsDivisionId}`);
  }
}