import { Route } from "@angular/router";
import { LegalIssueComponent } from "./legal-issue.component";
import { LegalIssueListComponent } from "./list/list.component";
import { LegalIssueDetailsComponent } from "./details/details.component";
import { LegalIssueViewComponent } from "./details/view/view.component";
import { LegalIssueEditComponent } from "./details/edit/edit.component";


export const routes: Route[] = [
  {
    path: '',
    component: LegalIssueComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: LegalIssueListComponent,
      },
      {
        path: ':id',
        component: LegalIssueDetailsComponent,
        children: [
          {
            path: '',
            pathMatch: 'full',
            component: LegalIssueViewComponent,
          },
          {
            path: 'edit',
            component: LegalIssueEditComponent,
          },
        ],
      },
    ],
  },
];
