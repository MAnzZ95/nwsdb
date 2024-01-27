import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { SharedModule } from "src/app/shared/shared.module";
import { MaterialModule } from "src/material.module";
import { routes } from "./land.routing";
import { LandsListComponent } from "./list/list.component";
import { LandComponent } from "./land.component";
import { NewLandComponent } from "./new/new.component";
import { LandDetailsComponent } from "./details/details.component";
import { LandViewComponent } from "./details/view/view.component";
import { LandEditComponent } from "./details/edit/edit.component";

@NgModule({
    declarations: [
     LandsListComponent,
     LandComponent,
     NewLandComponent,
     LandDetailsComponent,
     LandViewComponent,
     LandEditComponent
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
  export class LandModule {}