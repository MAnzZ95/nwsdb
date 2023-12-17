import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { SharedModule } from "src/app/shared/shared.module";
import { MaterialModule } from "src/material.module";
import { RmoListComponent } from "./list/list.component";
import { RmoNewComponent } from "./new/new.component";
import { RmoComponent } from "./rmo.component";
import { routes } from "./rmo.routing";

@NgModule({
    declarations: [
     RmoListComponent,
     RmoComponent,
     RmoNewComponent
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
  export class RmoModule {}