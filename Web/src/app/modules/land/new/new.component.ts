import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSelectChange } from '@angular/material/select';
import { Router } from '@angular/router';
import { District } from 'src/app/core/models/districts/district';
import { DsDivision } from 'src/app/core/models/ds-divisions/ds-divition';
import { GvDivision } from 'src/app/core/models/gv-divisions/gv-division';
import { LandType } from 'src/app/core/models/land-types/land-type';
import { OwnerShip } from 'src/app/core/models/owner-ships/owner-ship';
import { Province } from 'src/app/core/models/provinces/province';
import { Rmo } from 'src/app/core/models/rmos/rmo';
import { Rsc } from 'src/app/core/models/rscs/rsc';
import { SubCategory } from 'src/app/core/models/sub-categories/sub-category';
import { Wss } from 'src/app/core/models/wsses/wss';

@Component({
  selector: 'app-new',
  templateUrl: './new.component.html',
  styleUrls: ['./new.component.scss']
})
export class NewLandComponent implements OnInit {

  inputForm!: FormGroup;
  activeStatus = true;
  isActiveChecked = true;
  provinces: Province[] = [];
  districts: District[] = [];
  dsDivisions: DsDivision[] = [];
  gvDivisions: GvDivision[] = [];
  ownerShips: OwnerShip[] = [];
  rscs: Rsc[] =[];
  rmos: Rmo[] =[];
  wsses: Wss[] = [];
  categories: SubCategory[] = [];
  landTypes: LandType[] = [];


  constructor(
    private dialog: MatDialog,
    private inputFormBuilder: FormBuilder,
    private router: Router,
  ) {}

  ngOnInit() {
    this.inputForm = this.inputFormBuilder.group({
      landName:['',Validators.required],
      phone:['',Validators.required],
      area:['',Validators.required],
      address:['',Validators.required],
      province:['',Validators.required],
      district:['',Validators.required],
      dsDivision:['',Validators.required],
      gvDivision:['',Validators.required],
      ownerShip:[''],
      subCategory:[''],
      rsc:['',Validators.required],
      rmo:['', Validators.required],
      wss:['',Validators.required],
      landType:['',Validators.required],
      landStatus:['',Validators.required],
      legalStatus:['',Validators.required],
      remark:['']
    });
  }

  saveCompany() {
  }

  onActiveStatusChange(value: boolean) {
    this.activeStatus = value;
  }

  onCancel(): void {
    this.router.navigate([`/land`], {});
  }

  onProvinceChanged(event: MatSelectChange) {
    //this.selectedCompanyName = event.source.triggerValue;
  }

  onDistrictChanged(event: MatSelectChange) {

  }

  onDSDivisionChanged(event: MatSelectChange){

  }

  onGVDivisionChanged(event: MatSelectChange){

  }

  onRscChanged(event: MatSelectChange){

  }

  onRmoChanged(event: MatSelectChange){

  }

  onLegalIssueChanged(event: MatSelectChange){

  }
}
