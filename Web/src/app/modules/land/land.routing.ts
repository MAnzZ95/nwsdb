import { Route } from '@angular/router';
import { LandsListComponent } from './list/list.component';
import { LandComponent } from './land.component';
import { NewLandComponent } from './new/new.component';
import { LandViewComponent } from './details/view/view.component';
import { LandEditComponent } from './details/edit/edit.component';
import { LandDetailsComponent } from './details/details.component';

export const routes: Route[] = [
  {
    path: '',
    component: LandComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: LandsListComponent,
      },
      {
        path: 'new',
        component: NewLandComponent
      },
      {
        path: ':id',
        component: LandDetailsComponent,
        children: [
          {
            path: '',
            pathMatch: 'full',
            component: LandViewComponent,
          },
          {
            path: 'edit',
            component: LandEditComponent,
          },
        ],
      },
    ],
  },
];
