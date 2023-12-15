import { TableAction } from '@osos/core/enums/table-action';

export interface TableEmitData {
  type: TableAction;
  id: string;
}
