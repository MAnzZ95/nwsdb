import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AppConfigService } from 'src/app/app-config.service';
import { Land } from '../models/lands/land';
import { LayoutModule } from '@angular/cdk/layout';
import { SubCategory } from '../models/sub-categories/sub-category';

@Injectable({
  providedIn: 'root'
})
export class LandSubCategoryService {
  private config = inject(AppConfigService);
  private httpClient = inject(HttpClient);

  private apiUrl =`${this.config.apiUrl}/api/landSubCategories`;

  getSubCategoryDetails(): Observable<SubCategory[]> {
    return this.httpClient.get<SubCategory[]>(`${this.apiUrl}`);
  }

  getSubCategoryDetailsWithOData(
    ODataQueryParams: HttpParams
  ):Observable<SubCategory[]>{
    return this.httpClient.get<SubCategory[]>(`${this.apiUrl}`,{params: ODataQueryParams});
  }

  getSubCategoryDetailsById(subCategoryId: string): Observable<SubCategory>{
    return this.httpClient.get<SubCategory>(`${this.apiUrl}/${subCategoryId}`);
  }

  createSubCategory(subCategory: SubCategory): Observable<SubCategory>{
    return this.httpClient.post<SubCategory>(`${this.apiUrl}`,subCategory);
  }

  modifySubCategory(subCategory: SubCategory): Observable<SubCategory>{
    return this.httpClient.put<SubCategory>(`${this.apiUrl}`,subCategory);
  }

  getSubCategorysCount(): Observable<number>{
    return this.httpClient.get<number>(`${this.apiUrl}/count`);
  }

  removeSubCategory(subCategoryId: string): Observable<SubCategory> {
    return this.httpClient.delete<SubCategory>(`${this.apiUrl}/${subCategoryId}`);
  }
}
