import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { OwnerShip } from '../models/owner-ships/owner-ship';
import { LayoutModule } from '@angular/cdk/layout';

@Injectable({
  providedIn: 'root'
})
export class OwnerShipService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/ownerShips`;

  getOwnerShipDetails(): Observable<OwnerShip[]> {
    return this.httpClient.get<OwnerShip[]>(`${this.apiUrl}`);
  }

  getOwnerShipDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<OwnerShip[]>{
    return this.httpClient.get<OwnerShip[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getOwnerShipDetailsById(ownerShipId: string): Observable<OwnerShip>{
    return this.httpClient.get<OwnerShip>(`${this.apiUrl}/${ownerShipId}`);
  }

  createOwnerShip(ownerShip: OwnerShip): Observable<OwnerShip>{
    return this.httpClient.post<OwnerShip>(`${this.apiUrl}`,ownerShip);
  }

  modifyOwnerShip(ownerShip: OwnerShip): Observable<OwnerShip>{
    return this.httpClient.put<OwnerShip>(`${this.apiUrl}`,ownerShip);
  }

  getOwnerShipsCount(): Observable<number>{
    return this.httpClient.get<number>(`${this.apiUrl}/count`);
  }

  removeOwnerShip(ownerShipId: string): Observable<OwnerShip> {
    return this.httpClient.delete<OwnerShip>(`${this.apiUrl}/${ownerShipId}`);
  }
}
