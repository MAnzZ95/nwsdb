import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { SharedModule } from "src/app/shared/shared.module";
import { MaterialModule } from "src/material.module";
import { RscListComponent } from "./list/list.component";
import { RscNewComponent } from "./new/new.component";
import { RscComponent } from "./rsc.component";
import { routes } from "./rsc.routing";

@NgModule({
    declarations: [
     RscListComponent,
     RscComponent,
     RscNewComponent
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
  export class RscModule {}