
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
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
import { AppConfigService } from './app-config.service';
import { initializeApp } from './core/utils/app.init';
import { KeycloakAngularModule, KeycloakService } from 'keycloak-angular';
import { AuthGuard } from './core/utils/app.guard';

@NgModule({
    declarations: [
        AppComponent,
        SpinnerComponent,
        FullComponent,
        AppHeaderComponent
    ],
    providers: [
        AuthGuard,
        AppConfigService,
        {
            provide: LocationStrategy,
            useClass: PathLocationStrategy
        },
        {
            provide: APP_INITIALIZER,
            useFactory:initializeApp,
            multi:true,
            deps:[HttpClient,KeycloakService,AppConfigService]
        }
    ],    
    exports: [RouterModule],
    imports: [
        HttpClientModule,
        RouterModule,
        AppRoutingModule,
        KeycloakAngularModule,
        NoopAnimationsModule,
        DemoMaterialModule,
        SharedModule,
        AppSidebarComponent,
        RouterModule.forRoot(AppRoutes),
        BrowserModule,
        BrowserAnimationsModule,
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }
