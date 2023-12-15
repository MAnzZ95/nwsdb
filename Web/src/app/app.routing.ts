import { RouterModule, Routes } from '@angular/router';
import { FullComponent } from './layout/full/full.component';
import { NgModule } from '@angular/core';

export const AppRoutes: Routes = [
  {
    path: '',
    component: FullComponent,
    children: [
      {
        path: '',
        redirectTo: '/dashboard',
        pathMatch: 'full'
      },
      {
        path: 'land',
        loadChildren:
          () => import('./modules/land/land.module').then(m => m.LandModule)
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
      }
    ]
  }
];

@NgModule({
  imports:[RouterModule.forRoot(AppRoutes)],
  exports: [RouterModule],
})

export class AppRoutingModule{}
