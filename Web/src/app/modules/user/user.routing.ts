import { Route } from '@angular/router';
import { UserListComponent } from './list/list.component';
import { UserNewComponent } from './new/new.component';
import { UserViewComponent } from './details/view/view.component';
import { UserEditComponent } from './details/edit/edit.component';
import { UserComponent } from './user.component';
import { UserDetailsComponent } from './details/details.component';

export const routes: Route[] = [
  {
    path: '',
    component: UserComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: UserListComponent,
      },
      {
        path: 'new',
        component: UserNewComponent
      },
      {
        path: ':id',
        component: UserDetailsComponent,
        children: [
          {
            path: '',
            pathMatch: 'full',
            component: UserViewComponent,
          },
          {
            path: 'edit',
            component: UserEditComponent,
          },
        ],
      },
    ],
  },
];
