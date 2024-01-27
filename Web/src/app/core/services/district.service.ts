import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { District } from '../models/districts/district';
import { LayoutModule } from '@angular/cdk/layout';

@Injectable({
  providedIn: 'root'
})
export class DistrictService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/districts`;

  getDistrictDetails(): Observable<District[]> {
    return this.httpClient.get<District[]>(`${this.apiUrl}`);
  }

  getDistrictDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<District[]>{
    return this.httpClient.get<District[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getDistrictDetailsById(districtId: string): Observable<District>{
    return this.httpClient.get<District>(`${this.apiUrl}/${districtId}`);
  }

  createDistrict(district: District): Observable<District>{
    return this.httpClient.post<District>(`${this.apiUrl}`,district);
  }

  modifyDistrict(district: District): Observable<District>{
    return this.httpClient.put<District>(`${this.apiUrl}`,district);
  }

  getDistrictsCount(): Observable<number>{
    return this.httpClient.get<number>(`${this.apiUrl}/count`);
  }

  removeDistrict(districtId: string): Observable<District> {
    return this.httpClient.delete<District>(`${this.apiUrl}/${districtId}`);
  }

  getDistrictDetailsByProvinceId(provinceId: string): Observable<District[]>{
    return this.httpClient.get<District[]>(`${this.apiUrl}/${provinceId}`);
  }
}
