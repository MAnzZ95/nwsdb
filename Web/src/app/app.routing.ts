import { RouterModule, Routes } from '@angular/router';
import { FullComponent } from './layout/full/full.component';
import { NgModule } from '@angular/core';
import { AuthGuard } from './core/utils/app.guard';
import { WssListComponent } from './modules/wss/list/list.component';

export const AppRoutes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/dashboard'
  },
  {
    path: '',
    component: FullComponent,
    children: [
      {
        path: 'dashboard',
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
        canActivate: [AuthGuard], // Guard applied to a lazy-loaded module
      },
      {
        path: 'land',
        loadChildren:
          () => import('./modules/land/land.module').then(m => m.LandModule), 
          canActivate:[AuthGuard]
      },
      {
        path: 'legal-issue',
        loadChildren:
          () => import('./modules/legal-issue/legal-issue.module').then(m => m.LegalIssueModule), 
          canActivate:[AuthGuard]
      },
      {
        path: 'rsc',
        loadChildren:
          () => import('./modules/rsc/rsc.module').then(m => m.RscModule), 
          canActivate:[AuthGuard]
      },
      {
        path: 'rmo',
        loadChildren:
          () => import('./modules/rmo/rmo.module').then(m => m.RmoModule), 
          canActivate:[AuthGuard]
      },
      {
        path: 'wss',
        loadChildren:
          () => import('./modules/wss/wss.module').then(m => m.WssModule), 
          canActivate:[AuthGuard]
      },
      {
        path: 'user',
        loadChildren:
          () => import('./modules/user/user.module').then(m => m.UserModule), 
          canActivate:[AuthGuard]
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule), 
        canActivate:[AuthGuard]
      },
      {
        path: 'test',
        children: [
          { path: 'child1', component: WssListComponent },
          { path: 'child2', component: WssListComponent },
        ],
      },
      
    ]
  }
];

@NgModule({
  imports:[RouterModule.forRoot(AppRoutes)],
  exports: [RouterModule],
})

export class AppRoutingModule{}
