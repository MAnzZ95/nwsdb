import { SORT_ORDER } from './filteration.enum';

export interface TableColumn {
  name: string;
  displayName: string;
  path: string;
  isInsideObject: string;
  isDefault: boolean;
  allowSearch: boolean;
  allowSort: boolean;
}

export interface SortState {
  sortOrder: SORT_ORDER;
  sortProperty: TableColumn;
}
