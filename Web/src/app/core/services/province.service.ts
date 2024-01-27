import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { Province } from '../models/provinces/province';
import { LayoutModule } from '@angular/cdk/layout';

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/provinces`;

  getProvinceDetails(): Observable<Province[]> {
    return this.httpClient.get<Province[]>(`${this.apiUrl}`);
  }

  getProvinceDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<Province[]>{
    return this.httpClient.get<Province[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getProvinceDetailsById(provinceId: string): Observable<Province>{
    return this.httpClient.get<Province>(`${this.apiUrl}/${provinceId}`);
  }
}
