import { Route } from '@angular/router';
import { RmoListComponent } from './list/list.component';
import { RmoNewComponent } from './new/new.component';
import { RmoViewComponent } from './details/view/view.component';
import { RmoEditComponent } from './details/edit/edit.component';
import { RmoComponent } from './rmo.component';
import { RmoDetailsComponent } from './details/details.component';

export const routes: Route[] = [
  {
    path: '',
    component: RmoComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: RmoListComponent,
      },
      {
        path: 'new',
        component: RmoNewComponent
      },
      {
        path: ':id',
        component: RmoDetailsComponent,
        children: [
          {
            path: '',
            pathMatch: 'full',
            component: RmoViewComponent,
          },
          {
            path: 'edit',
            component: RmoEditComponent,
          },
        ],
      },
    ],
  },
];
