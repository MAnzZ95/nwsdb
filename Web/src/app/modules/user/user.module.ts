import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { SharedModule } from "src/app/shared/shared.module";
import { MaterialModule } from "src/material.module";
import { UserListComponent } from "./list/list.component";
import { UserNewComponent } from "./new/new.component";
import { UserComponent } from "./user.component";
import { routes } from "./user.routing";

@NgModule({
    declarations: [
     UserListComponent,
     UserComponent,
     UserNewComponent
    ],
    imports: [
      RouterModule.forChild(routes),
      FormsModule,
      ReactiveFormsModule,
      MaterialModule,
      CommonModule,
      SharedModule,
    ],
    exports: [RouterModule],
  })
  export class UserModule {}