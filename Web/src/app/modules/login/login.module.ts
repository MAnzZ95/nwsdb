import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { SharedModule } from "src/app/shared";
import { MaterialModule } from "src/material.module";
import { LoginComponent } from "./login.component";

@NgModule({
    declarations: [
        LoginComponent
    ],
    imports: [
      FormsModule,
      ReactiveFormsModule,
      MaterialModule,
      CommonModule,
      SharedModule,
    ],
    exports: [RouterModule],
  })
  export class LoginModule {}