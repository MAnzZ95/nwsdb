
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { SpinnerComponent } from './shared/spinner.componen';
import { FullComponent } from './layout/full/full.component';
import { AppHeaderComponent } from './layout/full/header/header.component';
import { DemoMaterialModule } from './demo-material-module';
import { SharedModule } from './shared/shared.module';
import { AppSidebarComponent } from './layout/full/sidebar/sidebar.component';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutes } from './app.routing';

@NgModule({
    declarations: [
        AppComponent,
        SpinnerComponent,
        FullComponent,
        AppHeaderComponent
    ],
    providers: [{
        provide: LocationStrategy,
        useClass: PathLocationStrategy
        }],
    bootstrap: [AppComponent],
    exports: [RouterModule],
    imports: [
        HttpClientModule,
        RouterModule,
        AppRoutingModule,
        NoopAnimationsModule,
        DemoMaterialModule,
        SharedModule,
        AppSidebarComponent,
        RouterModule.forRoot(AppRoutes),
        BrowserModule,
        BrowserAnimationsModule,
    ]
})
export class AppModule { }
