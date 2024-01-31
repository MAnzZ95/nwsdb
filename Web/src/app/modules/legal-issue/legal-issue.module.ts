import { NgModule } from "@angular/core";
import { LegalIssueComponent } from "./legal-issue.component";
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MaterialModule } from "src/material.module";
import { CommonModule } from "@angular/common";
import { SharedModule } from "src/app/shared";
import { LegalIssueListComponent } from "./list/list.component";
import { routes } from "./legal-issue.routing";
import { LegalIssueDetailsComponent } from "./details/details.component";

@NgModule({
    declarations: [
        LegalIssueComponent,
        LegalIssueListComponent,
        LegalIssueDetailsComponent
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
  export class LegalIssueModule {}