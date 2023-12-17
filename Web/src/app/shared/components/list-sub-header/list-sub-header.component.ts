import {
  CdkDragDrop,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import {
  Component,
  ElementRef,
  HostListener,
  OnDestroy,
  OnInit,
  ViewChild,
  inject,
} from '@angular/core';
import { MatRadioChange, MatRadioGroup } from '@angular/material/radio';
import { MatDrawer } from '@angular/material/sidenav';
import { Subscription, map, of } from 'rxjs';

import { CommonFilterationsService } from 'src/app/core/services/filterations/common-filterations/common-filterations.service';
import { SortState, TableColumn } from 'src/app/core/models/filterations/filteration.model';
import { SORT_ORDER } from 'src/app/core/models/filterations/filteration.enum';

@Component({
  selector: 'app-list-sub-header',
  templateUrl: './list-sub-header.component.html',
  styleUrls: ['./list-sub-header.component.scss'],
})
export class ListSubHeaderComponent implements OnInit, OnDestroy {

  private status = false;
  public showTitle = true;
  private deselectAll = true;
  private isSelectedSort = false;
  private isAvailableTableData = false;

  public SORT_ORDER = SORT_ORDER;
  public sortOrder = SORT_ORDER.DESC;
  private sortProperty!: TableColumn;
  private selectedProp!: MatRadioChange;
  public tableColumnDetails: TableColumn[] = [];
  public defaultTableColumn: TableColumn[] = [];
  public availableTableColumn: TableColumn[] = [];
  private dataSubscription!: Subscription;

  @ViewChild('sort') sortDrawer!: MatDrawer;
  @ViewChild('filter') filterDrawer!: MatDrawer;
  @ViewChild('groupby') groupbyDrawer!: MatDrawer;
  @ViewChild('radioGroup') radioGroup!: MatRadioGroup;
  @ViewChild('columnselection') columnSelectionDrawer!: MatDrawer;

  constructor(
    private el: ElementRef,
    private commonFilterationsService: CommonFilterationsService
  ) {}

  ngOnInit(): void {
    this.getColumnDetails();
  }

  ngOnDestroy(): void {
    this.isAvailableTableData = false;
    if (this.dataSubscription) {
      this.dataSubscription.unsubscribe();
    }
  }

  @HostListener('document:click', ['$event'])
  onClick(event: Event) {
    if (!this.el.nativeElement.contains(event.target)) {
      this.closeDrawer();
    }
  }

  getColumnDetails() {
    const subscription = this.commonFilterationsService
      .getColumnsDetails()
      .subscribe((data: string | any[]) => {
        const shouldGoInside =
          Array.isArray(data) && data.length > 0 && !this.isAvailableTableData;
        if (shouldGoInside) {
          this.isAvailableTableData = true;
          this.tableColumnDetails = data;
          this.assignColumnDetails();
        }
      });
    this.dataSubscription?.add(subscription);
  }

  filterDefaultList(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (filterValue.trim().length === 0) {
      this.defaultTableColumn = this.tableColumnDetails.filter(
        col => col.isDefault
      );
    } else {
      this.defaultTableColumn = this.defaultTableColumn.filter(col => {
        return col.displayName
          .toLowerCase()
          .includes(filterValue.toLowerCase());
      });
    }
  }

  filterAvailableList(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    if (filterValue.trim().length === 0) {
      this.availableTableColumn = this.tableColumnDetails.filter(
        col => !col.isDefault
      );
    } else {
      this.availableTableColumn = this.availableTableColumn.filter(col => {
        return col.displayName
          .toLowerCase()
          .includes(filterValue.toLowerCase());
      });
    }
  }

  assignColumnDetails() {
    of(this.tableColumnDetails)
      .pipe(
        map(columns => {
          const defaultColumns = columns.filter(col => col.isDefault);
          const availableColumns = columns.filter(col => !col.isDefault);
          return { defaultColumns, availableColumns };
        })
      )
      .subscribe(({ defaultColumns, availableColumns }) => {
        this.defaultTableColumn = defaultColumns;
        this.availableTableColumn = availableColumns;
      });
  }

  resetColumnDetails() {
    this.commonFilterationsService.setColumnsDetails(
      this.tableColumnDetails.filter(column => column.isDefault)
    );
  }

  drop(event: CdkDragDrop<TableColumn[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
    this.commonFilterationsService.setColumnsDetails(this.defaultTableColumn);
  }

  toggle() {
    this.showTitle = !this.showTitle;
  }

  setSortOrder(sortOrder: SORT_ORDER) {
    this.sortOrder = sortOrder;
    this.setSortDetails();
  }

  setProperty(prop: MatRadioChange) {
    this.isSelectedSort = true;
    this.deselectAll = false;
    this.sortProperty = prop.value;
    this.sortDrawer.close();
    this.setSortDetails();
    this.selectedProp = prop;
  }

  setSortDetails() {
    const sortState: SortState = {
      sortOrder: this.sortOrder,
      sortProperty: this.sortProperty,
    };
    this.commonFilterationsService.setIsSelectedSort(true);
    this.commonFilterationsService.setSortState(sortState);
  }

  toggleSelectAllChecked() {
    if (!this.isSelectedSort) {
      this.deselectAll = true;
    }
  }

  shouldCheckRadioButton(prop: TableColumn): boolean {
    let checkedValue = false;
    if (this.isSelectedSort) {
      checkedValue = !this.deselectAll && prop === this.selectedProp.value;
    } else {
      this.sortProperty = this.defaultTableColumn.find(
        x => x.name == 'createdDate'
      ) as TableColumn;
      checkedValue = prop === this.sortProperty;
    }
    return checkedValue;
  }

  handleRefreshSortBy() {
    this.isSelectedSort = false;
    this.deselectAll = true;
    this.sortOrder = SORT_ORDER.DESC;
    this.commonFilterationsService.setIsSelectedSort(this.isSelectedSort);
    this.sortDrawer.close();
  }

  closeDrawer() {
    this.sortDrawer.close();
  }

  handleRefreshColumnSelection() {
    this.resetColumnDetails();
    this.assignColumnDetails();
  }

  handleGroupBy() {
    this.sortDrawer.close();
    this.groupbyDrawer.toggle();
    this.columnSelectionDrawer.close();
  }

  handleSortBy() {
    this.columnSelectionDrawer.close();
    this.groupbyDrawer.close();
    this.filterDrawer.close();
    this.sortDrawer.toggle();
    this.toggleSelectAllChecked();
  }

  handleColumnSelection() {
    this.sortDrawer.close();
    this.groupbyDrawer.close();
    this.filterDrawer.close();
    this.columnSelectionDrawer.toggle();
  }

  handleFilterBy() {
    this.sortDrawer.close();
    this.groupbyDrawer.close();
    this.columnSelectionDrawer.close();
    this.filterDrawer.toggle();
  }

  clickEvent() {
    this.status = !this.status;
  }
}
