import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { User } from '../models/users/user';
import { UserType } from '../models/user-types/user-type';

@Injectable({
  providedIn: 'root'
})
export class UserTypeService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/userTypes`;

  getUserDetails(): Observable<UserType> {
    return this.httpClient.get<UserType>(`${this.apiUrl}`);
  }

//   getUserDetailsWithOData(
//     ODataQueryParams: HttpParams
//   ):Observable<User[]>{
//     return this.httpClient.get<User[]>(`${this.apiUrl}`,{params: ODataQueryParams});
//   }

//   getUserDetailsById(wssId: string): Observable<User>{
//     return this.httpClient.get<User>(`${this.apiUrl}/${wssId}`);
//   }

//   createUser(user: User): Observable<User>{
//     return this.httpClient.post<User>(`${this.apiUrl}`,user);
//   }

//   modifyUser(user: User): Observable<User>{
//     return this.httpClient.put<User>(`${this.apiUrl}`,user);
//   }

//   getUsersCount(): Observable<number>{
//     return this.httpClient.get<number>(`${this.apiUrl}/count`);
//   }

//   removeUser(userId: string): Observable<User> {
//     return this.httpClient.delete<User>(`${this.apiUrl}/${userId}`);
//   }
}
