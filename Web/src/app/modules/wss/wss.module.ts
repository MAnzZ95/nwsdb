import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { SharedModule } from "src/app/shared/shared.module";
import { MaterialModule } from "src/material.module";
import { WssListComponent } from "./list/list.component";
import { WssNewComponent } from "./new/new.component";
import { WssComponent } from "./wss.component";
import { routes } from "./wss.routing";

@NgModule({
    declarations: [
     WssListComponent,
     WssComponent,
     WssNewComponent
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
  export class WssModule {}