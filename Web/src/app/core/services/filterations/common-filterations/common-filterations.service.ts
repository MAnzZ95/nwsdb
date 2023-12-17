import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {
  SortState,
  TableColumn,
} from '../../../models/filterations/filteration.model';

@Injectable({
  providedIn: 'root',
})
export class CommonFilterationsService {
  sortState!: SortState;

  private tableColumDetails = new BehaviorSubject<TableColumn[]>([]);
  _tableColumDetails = this.tableColumDetails.asObservable();

  private sortByProperty = new BehaviorSubject<SortState>(this.sortState);
  _sortByProperty = this.sortByProperty.asObservable();

  private isSelectedSort = new BehaviorSubject<boolean>(false);
  _isSelectedSort = this.isSelectedSort.asObservable();

  getColumnsDetails(): Observable<TableColumn[]> {
    return this._tableColumDetails;
  }

  setColumnsDetails(columns: TableColumn[]) {
    this.tableColumDetails.next(columns);
  }

  getSortState(): Observable<SortState> {
    return this._sortByProperty;
  }

  setSortState(sortState: SortState) {
    this.sortByProperty.next(sortState);
  }

  getIsSelectedSort(): Observable<boolean> {
    return this._isSelectedSort;
  }

  setIsSelectedSort(isSelectedSort: boolean) {
    this.isSelectedSort.next(isSelectedSort);
  }
}
