import { Injectable } from '@angular/core';

export interface Menu {
  state: string;
  name: string;
  type: string;
  icon: string;
  children?: Menu[] | undefined;
}

const MENUITEMS = [
  { state: 'dashboard', name: 'Dashboard', type: 'link', icon: 'av_timer' },
  { state: 'land', type: 'link', name: 'Lands', icon: 'crop_7_5' },
  // { state: 'legal-issue', type: 'link', name: 'Legal Issues', icon: 'announcement' },
  { state: 'rsc', type: 'link', name: 'RS Center', icon: 'view_comfy' },
  { state: 'rmo', type: 'link', name: 'RM Office', icon: 'view_list' },
  { state: 'wss', type: 'link', name: 'WS Schemes', icon: 'view_headline' },
  { state: 'user', type: 'link', name: 'Users', icon: 'tab' },
  { 
    state: 'test', 
    type: 'expandable', 
    name: 'Stepper', 
    icon: 'web',
    children: [
      { state: 'child1', type: 'link', name: 'Child 1', icon: 'child_care' },
      { state: 'child2', type: 'link', name: 'Child 2', icon: 'child_care' }
    ]
   }
];

@Injectable()
export class MenuItems {
  getMenuitem(): Menu[] {
    return MENUITEMS;
  }
}
