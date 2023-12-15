import { HttpParams } from '@angular/common/http';
import { Pagination } from '../datatables/pagination';
import { PageOptions } from './filteration.constants';
import { SORT_ORDER } from './filteration.enum';
import { SortState } from './filteration.model';

export class OsosODataQuery implements Pagination {
  currentPage: number = PageOptions.currentPage;
  totalPages: number = PageOptions.totalPages;
  pageSize: number = PageOptions.pageSize;
  pageSizeOptions: number[] = PageOptions.pageSizeOptions;

  private params!: HttpParams;

  oDataPaginationQuery(): HttpParams {
    this.params = this.params.append('$top', this.pageSize.toString());
    this.params = this.params.append(
      '$skip',
      ((this.currentPage - 1) * this.pageSize).toString()
    );
    return this.params;
  }

  oDataSortingQuery(sortState: SortState): HttpParams {
    const isDescOrder = sortState.sortOrder === SORT_ORDER.DESC;
    this.params = this.params.append(
      '$orderby',
      `${sortState.sortProperty.path} ${isDescOrder ? 'desc' : 'asc'}`
    );
    return this.params;
  }

  clearOdataParam() {
    this.params = new HttpParams();
  }

  filterByColumns(columns: any[]) {
    this.clearOdataParam();

    if (columns && columns.length > 0) {
      const filterClauses: any[] = [];

      for (const columnObj of columns) {
        const columnName: string = columnObj.column;
        const value: any = columnObj.value;

        if (columnName && value !== undefined) {
          const filterClause =
            typeof value === 'string'
              ? `${columnName} eq '${value}'`
              : `${columnName} eq ${value}`;

          filterClauses.push(filterClause);
        }
      }

      if (filterClauses.length > 0) {
        const filterString = filterClauses.join(' and ');
        this.params = this.params.append('$filter', filterString);
      }
    }

    return this.params;
  }
}
