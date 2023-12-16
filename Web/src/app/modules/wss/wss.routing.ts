import { Route } from '@angular/router';
import { WssListComponent } from './list/list.component';
import { WssNewComponent } from './new/new.component';
import { WssViewComponent } from './details/view/view.component';
import { WssEditComponent } from './details/edit/edit.component';
import { WssComponent } from './wss.component';
import { WssDetailsComponent } from './details/details.component';

export const routes: Route[] = [
  {
    path: '',
    component: WssComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: WssListComponent,
      },
      {
        path: 'new',
        component: WssNewComponent
      },
      {
        path: ':id',
        component: WssDetailsComponent,
        children: [
          {
            path: '',
            pathMatch: 'full',
            component: WssViewComponent,
          },
          {
            path: 'edit',
            component: WssEditComponent,
          },
        ],
      },
    ],
  },
];
