import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  inject,
} from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { T } from '@fullcalendar/core/internal-common';
import { Status } from '@osos/core/enums';
import { Grant } from '@osos/core/enums/grant';
import { TableAction } from '@osos/core/enums/table-action';
import { TableColumnType } from '@osos/core/enums/table-column-type';
import { getStatusClass } from '@osos/core/helpers';
import { Column } from '@osos/core/models/datatables/column';
import { Pagination } from '@osos/core/models/datatables/pagination';
import { AccessService } from '@osos/core/services/access.service';
import { checkAccess } from '@osos/core/utils';
import { TableEmitData } from './../../../core/models/datatables/table-emit-data';

@Component({
  selector: 'app-datatable',
  templateUrl: './datatable.component.html',
  styleUrls: ['./datatable.component.scss'],
})
export class DatatableComponent implements OnInit {
  @Output() clickEvent = new EventEmitter<TableEmitData>();
  @Output() paginationEvent = new EventEmitter<PageEvent>();
  @Input() tableColumns: Array<Column> = [];
  @Input() dataSource: MatTableDataSource<T> = new MatTableDataSource();
  @Input() grant = Grant;
  @Input() pagination!: Pagination;
  tableAction = TableAction;
  displayedColumns: Array<string> = [];
  statusEnum = Status;
  columnTypeEnum = TableColumnType;
  private accessService = inject(AccessService);

  ngOnInit() {
    this.displayedColumns = this.tableColumns.map(c => c.columnDef);
  }

  checkAccess(grant: Grant, key: string) {
    return checkAccess(key, grant, this.accessService);
  }

  actionEmitEvent(type: TableAction, id: string) {
    this.clickEvent.emit({ type: type, id: id });
  }

  pageChangeEmitEvent(event: PageEvent) {
    this.paginationEvent.emit(event);
  }

  selectStatusClass(status: Status) {
    return getStatusClass(status);
  }
}
