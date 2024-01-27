import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { Land } from '../models/lands/land';
import { LayoutModule } from '@angular/cdk/layout';
import { LandType } from '../models/land-types/land-type';

@Injectable({
  providedIn: 'root'
})
export class LandTypeService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/landTypes`;

  getLandTypeDetails(): Observable<LandType[]> {
    return this.httpClient.get<LandType[]>(`${this.apiUrl}`);
  }

  getLandTypeDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<LandType[]>{
    return this.httpClient.get<LandType[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getLandTypeDetailsById(landTypeId: string): Observable<LandType>{
    return this.httpClient.get<LandType>(`${this.apiUrl}/${landTypeId}`);
  }

  createLandType(landType: LandType): Observable<LandType>{
    return this.httpClient.post<LandType>(`${this.apiUrl}`,landType);
  }

  modifyLandType(landType: LandType): Observable<LandType>{
    return this.httpClient.put<LandType>(`${this.apiUrl}`,landType);
  }

  getLandTypesCount(): Observable<number>{
    return this.httpClient.get<number>(`${this.apiUrl}/count`);
  }

  removeLandType(landTypeId: string): Observable<LandType> {
    return this.httpClient.delete<LandType>(`${this.apiUrl}/${landTypeId}`);
  }
}
