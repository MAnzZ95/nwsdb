import { from } from "rxjs";
import { ButtonComponent } from "./button/button.component";
import { DeletePopupCommonComponent } from "./dialog/delete-popup-common/delete-popup-common.component";
import { DialogComponent } from "./dialog/dialog.component";
import { ListSubHeaderComponent } from "./list-sub-header/list-sub-header.component";
import { SwitchbaseComponent } from "./switchbase/switchbase.component";

export * from './list-sub-header/list-sub-header.component';
export * from './dialog/delete-popup-common/delete-popup-common.component';
export * from './dialog/dialog.component';
export * from './button/button.component';
export * from './switchbase/switchbase.component';

export const COMPONENTS = [
    ListSubHeaderComponent,
    DeletePopupCommonComponent,
    DialogComponent,
    ButtonComponent,
    SwitchbaseComponent
]