import { HttpParams } from '@angular/common/http';
import { ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { OsosODataQuery } from 'src/app/core/models/filterations/OsosODataQuery';
import { SORT_ORDER } from 'src/app/core/models/filterations/filteration.enum';
import { SortState, TableColumn } from 'src/app/core/models/filterations/filteration.model';
import { Rmo } from 'src/app/core/models/rmos/rmo';
import { ColumnDetailsService } from 'src/app/core/services/filterations/column-details/column-details.service';
import { CommonFilterationsService } from 'src/app/core/services/filterations/common-filterations/common-filterations.service';
import { RmoService } from 'src/app/core/services/rmo.service';
import { SnackBarNotificationService } from 'src/app/core/services/snack-notification.service';
import { DeletePopupCommonComponent } from 'src/app/shared/components';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class RmoListComponent {

  dataSource: MatTableDataSource<Rmo>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  dataQuery = new OsosODataQuery();
  lands: Rmo[] = [];
  sortState!: SortState;
  subscriber!: Subscription;
  isSelectedSorting = false;
  disableCompanyCreation = false;
  activeCompanyCount = 0;
  companyCountFromLicensingInfo = 0;
  tableColumnData!: TableColumn[];
  displayedColumns: string[] = [
    'action',
    'name',
    'rmoNumber',
    'rscName',
    'mobile',
    'address',
    'createdDate',
    'createdby',
    'status',
  ];
  subscription = new Subscription();

  tableColumn: TableColumn = {
    name: 'CreatedDate',
    displayName: 'CreatedDate',
    path: 'CreatedDate',
    isInsideObject: 'false',
    isDefault: false,
    allowSearch: true,
    allowSort: true,
  };
  isDescOrder: SortState = {
    sortOrder: SORT_ORDER.DESC,
    sortProperty: this.tableColumn,
  };

  constructor(
    private rmoService: RmoService,
    private cdr: ChangeDetectorRef,
    private commonFilterationsService: CommonFilterationsService,
    private columnDetailsService: ColumnDetailsService,
    private dialog: MatDialog,
    private snackBarNotification: SnackBarNotificationService,
  ) {
    this.dataSource = new MatTableDataSource<Rmo>([]);
  }

  async ngOnInit() {
    this.getColumnDetails();
    this.getAllRmosCount();
    this.isSelectedSort();
    this.getAllLandsAsync();
    this.cdr.detectChanges();
  }

  getAllLandsAsync() {
    const sub = this.rmoService.getRmoDetails().subscribe(response => {
      const data = Array.isArray(response) ? response : [response];
      this.dataSource.data = data;
      this.lands = data;
    });
    this.subscription.add(sub);
  }

  getStatusClass(status: number): string {
    if (status === 0) {
      return 'status-available';
    } else if (status === 1) {
      return 'status-not-available';
    } else if (status === 2) {
      return 'status-delete';
    } else {
      return '';
    }
  }

  getAllRmosCount() {
    const sub = this.rmoService.getRmosCount().subscribe(response => {
      if (response) {
        this.dataQuery.totalPages = response;
        this.clearExistingParamQuery();
        let oDataQuery = this.dataQuery.oDataPaginationQuery();
        oDataQuery = this.dataQuery.oDataSortingQuery(this.isDescOrder);
        this.selectAllLandsWithPagination(oDataQuery);
      }
    });
    this.subscription.add(sub);
  }

  getColumnDetails() {
    // this.columnDetailsService
    //   .getUserProfileColumnDetails()
    //   .subscribe((data: TableColumn[]) => {
    //     if (data) {
    //       this.tableColumnData = data;
    //       this.setColumnDataForSubHeader();
    //     }
    //   });
  }

  setColumnDataForSubHeader() {
    this.commonFilterationsService.setColumnsDetails(this.tableColumnData);
  }

  pageChanged(event: PageEvent) {
    this.dataQuery.currentPage = event.pageIndex + 1;
    this.dataQuery.pageSize = event.pageSize;
    this.clearExistingParamQuery();
    let oDataQuery = this.dataQuery.oDataPaginationQuery();
    oDataQuery = this.dataQuery.oDataSortingQuery(this.isDescOrder);
    this.selectAllLandsWithPagination(oDataQuery);
  }

  async selectAllLandsWithPagination(ODataQuery: HttpParams) {
    const sub = this.rmoService
      .getRmoDetailsWithOData(ODataQuery)
      .subscribe(response => {
        if (response) {
          const data = Array.isArray(response) ? response : [response];
          this.dataSource.data = data;
          this.dataSource = new MatTableDataSource(data);
          this.dataSource.sort = this.sort;
          this.cdr.detectChanges();
        }
      });
    this.subscription.add(sub);
  }

  generateSortParam(sort: SortState) {
    return this.dataQuery.oDataSortingQuery(sort);
  }

  generatePaginationParam() {
    return this.dataQuery.oDataPaginationQuery();
  }

  clearExistingParamQuery() {
    this.dataQuery.clearOdataParam();
  }

  isSelectedSort() {
    const sub = this.commonFilterationsService
      .getIsSelectedSort()
      .subscribe(isSelectedSorting => {
        if (this.isSelectedSorting && !isSelectedSorting) {
          this.dataQuery = new OsosODataQuery();
          this.getAllRmosCount();
        }
        this.isSelectedSorting = isSelectedSorting;
      });
    this.subscription.add(sub);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  removeCompany(id: string) {
    let deleteMessage = '';
    
    const dialogRef = this.dialog.open(DeletePopupCommonComponent, {
      data: {
        message: deleteMessage,
      },
      height: 'auto',
      width: '40em',
      panelClass: 'delete-dialog',
    });
    dialogRef.afterClosed().subscribe((result: string | undefined) => {
      if (result) {
        const sub = this.rmoService.removeRmo(id).subscribe({
          next: () => {
            this.snackBarNotification.show(
              'success',
              'land deleted successfully'
            );
            this.getAllRmosCount();
          },
        });
        this.subscription.add(sub);
      }
    });
  }
}
