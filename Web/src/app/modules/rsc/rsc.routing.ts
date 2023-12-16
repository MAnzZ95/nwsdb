import { Route } from '@angular/router';
import { RscListComponent } from './list/list.component';
import { RscNewComponent } from './new/new.component';
import { RscViewComponent } from './details/view/view.component';
import { RscEditComponent } from './details/edit/edit.component';
import { RscComponent } from './rsc.component';
import { RscDetailsComponent } from './details/details.component';

export const routes: Route[] = [
  {
    path: '',
    component: RscComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: RscListComponent,
      },
      {
        path: 'new',
        component: RscNewComponent
      },
      {
        path: ':id',
        component: RscDetailsComponent,
        children: [
          {
            path: '',
            pathMatch: 'full',
            component: RscViewComponent,
          },
          {
            path: 'edit',
            component: RscEditComponent,
          },
        ],
      },
    ],
  },
];
