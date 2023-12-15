import { TableColumnType } from '@osos/core/enums/table-column-type';
import { Action } from './action';

export interface Column {
  columnDef: string;
  header: string;
  cell: (row: any) => any;
  isLink?: boolean;
  url?: string;
  actions?: Action[];
  type: TableColumnType;
}
