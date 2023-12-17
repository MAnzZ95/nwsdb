import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TableColumn } from '../../../models/filterations/filteration.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ColumnDetailsService {
  constructor(private http: HttpClient) {}

  // getUserProfileColumnDetails(): Observable<TableColumn[]> {
  //   return this.http.get<TableColumn[]>(
  //     '../../../../assets/data/configuration/userProfiles.json'
  //   );
  // }
}
