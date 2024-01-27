import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSelectChange } from '@angular/material/select';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { District } from 'src/app/core/models/districts/district';
import { DsDivision } from 'src/app/core/models/ds-divisions/ds-divition';
import { GvDivision } from 'src/app/core/models/gv-divisions/gv-division';
import { LandType } from 'src/app/core/models/land-types/land-type';
import { Land } from 'src/app/core/models/lands/land';
import { LandAprovalStatus } from 'src/app/core/models/lands/land-approval-status';
import { LandStatus } from 'src/app/core/models/lands/land-status';
import { OwnerShip } from 'src/app/core/models/owner-ships/owner-ship';
import { Province } from 'src/app/core/models/provinces/province';
import { Rmo } from 'src/app/core/models/rmos/rmo';
import { Rsc } from 'src/app/core/models/rscs/rsc';
import { SubCategory } from 'src/app/core/models/sub-categories/sub-category';
import { Wss } from 'src/app/core/models/wsses/wss';
import { DistrictService } from 'src/app/core/services/district.service';
import { DsDivisionService } from 'src/app/core/services/ds-division.service';
import { GnDivisionService } from 'src/app/core/services/gn-division.service';
import { LandSubCategoryService } from 'src/app/core/services/land-sub-category.service';
import { LandTypeService } from 'src/app/core/services/land-type.service';
import { LandService } from 'src/app/core/services/land.service';
import { OwnerShipService } from 'src/app/core/services/owner-ship.service';
import { ProvinceService } from 'src/app/core/services/province.service';
import { RmoService } from 'src/app/core/services/rmo.service';
import { RscService } from 'src/app/core/services/rsc.service';
import { SnackBarNotificationService } from 'src/app/core/services/snack-notification.service';
import { WssService } from 'src/app/core/services/wss.service';
import { DialogComponent } from 'src/app/shared/components';
const EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class LandEditComponent implements OnInit {
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
  subscriber!: Subscription;
  isLegal = true;
  showDialog = true;
  subject = new Subject<boolean>();
  data!: Land;

  constructor(
    private dialog: MatDialog,
    private inputFormBuilder: FormBuilder,
    private router: Router,
    private landService: LandService,
    private rscService: RscService,
    private rmoService:RmoService,
    private provinceService: ProvinceService,
    private districtService: DistrictService,
    private wssService: WssService,
    private dsDivisionService: DsDivisionService,
    private gnDivisionService: GnDivisionService,
    private ownerShipService: OwnerShipService,
    private landTypeService: LandTypeService,
    private landSubCategoryService: LandSubCategoryService,
    private snackBarNotification: SnackBarNotificationService,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
  ) {}


  ngOnInit() {
    this.inputForm = this.inputFormBuilder.group({
      landName:[null,Validators.required],
      phone:[null,Validators.required],
      area:[null,Validators.required],
      address:[null,Validators.required],
      province:[null,Validators.required],
      district:[null],
      dsDivision:[null],
      gvDivision:[null],
      ownerShip:[null],
      subCategory:[{value: null, disabled: true}],
      rsc:[null,Validators.required],
      rmo:[null],
      wss:[null],
      landType:[null,Validators.required],
      legalStatus:[null,Validators.required],
      remark:[null]
    });

    this.getProvinces();
    this.getRscs();
    this.getLandTypes();
    this.getOwnerShips();
    this.getLandDetails();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(DialogComponent, {
      panelClass: 'delete-dialog',
      data: {
        title: 'cancel',
        content:'There is unsaved, changes do you need to cancel?',
        icon: 'warning_amber',
        cancel: 'osos-st btn-outline form-control',
        done: 'osos-st btn-save form-control',
        iconClass: 'mr-5 attention-icon',
        iconColor: 'warn',
      },
    });
    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.subject.next(true);
      } else {
        this.subject.next(false);
      }
    });
  }

  private markFormControlsAsTouched() {
    this.inputForm.markAsTouched();
  }

  private getIdFromUrl(): string | null {
    const urlParts = window.location.href.split('/');
    const landIdIndex = urlParts.indexOf('legal-issue') + 1;
    if (urlParts.length > landIdIndex) {
      return urlParts[landIdIndex];
    }
    return null;
  }

  private getLandIdFromRouting(): string {
    const id: string = this.route.snapshot.params['id'];
    return id;
  }

  getLandDetails() {
    const id =this.getIdFromUrl()?? '';
    console.log(id);
    const sub = this.landService.getLandDetailsById(id).subscribe({
      next: response => {
        this.data = response;
        if (this.data !== null) {
          this.getDistrictsByProviceId(this.data.provinceId);
          this.getDsDivisionByDistrictId(this.data.districId);
          this.getGvDivisionByDsDivisionId(this.data.gSDivisionId);
          this.getRmosByRscId(this.data.rscId);
          this.getWssesByRmoId(this.data.rmoId);
          this.getLandSubCategories();
          this.landDetailsInForm(this.data);
          this.cdr.detectChanges();
        }
      },
    });
    this.subscriber.add(sub);
  }

  landDetailsInForm(land: Land) {
    this.inputForm.patchValue({
      id: land.id,
      landType: land.landTypeId,
      landName: land.landName,
      phone:land.phone,
      area:land.landArea,
      address: land.address,
      province: land.provinceId,
      district: land.districId,
      dsDivision: land.dSDivisionId,
      gvDivision: land.gSDivisionId,
      ownerShip: land.ownerShipId,
      subCategory: land.subCategoryId,
      rsc: land.rscId,
      rmo: land.rmoId,
      wss: land.wssId,
      legalStatus: land.isLegal? "0":"1",
      remark: land.remark,
      latitude: land.latitude,
      longitude: land.longitude
    });
  }

  public MapToLand(): Land {
    const landData = this.inputForm.value;
    const id = this.getIdFromUrl()??EMPTY_GUID;
    const landObject: Land = {
      id: id ?? EMPTY_GUID,
      landName: landData.landName,
      landTypeId: landData.landType,
      ownerShipId: landData.ownerShip,
      provinceId: landData.province,
      districId: landData.district,
      dSDivisionId: landData.dsDivision,
      gSDivisionId: landData.gvDivision,
      subCategoryId: landData.subCategory,
      wssId: landData.wss,
      rmoId: landData.rmo,
      rscId: landData.rsc,
      phone: landData.phone,
      landArea: landData.area,
      address: landData.address,
      latitude: landData.latitude,
      longitude: landData.longitude,
      landStatus: this.activeStatus ? LandStatus.Active : LandStatus.Inactive,
      landAprovalStatus: this.isLegal ? LandAprovalStatus.Approved : LandAprovalStatus.Pending,
      isLegal: this.isLegal,
      remark: landData.remark
    }

    return landObject;
  }

  onActiveStatusChange(value: boolean) {
    this.activeStatus = value;
  }

  onCancel(): void {
    this.router.navigate([`/land`], {});
  }

  onProvinceChanged(event: MatSelectChange) {
    if(event.value!=null)
    {
      this.inputForm.get('districts')?.enable();
      this.getDistrictsByProviceId(event.value);
    }
    else{
      this.inputForm.get('districts')?.reset();
      this.inputForm.get('districts')?.disable();
    }    
  }

  onDistrictChanged(event: MatSelectChange) {
    if(event.value!=null)
    {
      this.inputForm.get('dsDivisions')?.enable();
      this.getDsDivisionByDistrictId(event.value);
    }
    else{
      this.inputForm.get('dsDivisions')?.reset();
      this.inputForm.get('dsDivisions')?.disable();
    }  
    
  }

  onDSDivisionChanged(event: MatSelectChange){
    if(event.value!=null)
    {
      this.inputForm.get('gvDivision')?.enable();
      this.getGvDivisionByDsDivisionId(event.value);
    }
    else{
      this.inputForm.get('gvDivision')?.reset();
      this.inputForm.get('gvDivision')?.disable();
    } 
  }

  onRscChanged(event: MatSelectChange){
    if(event.value!=null)
    {
      this.inputForm.get('rsc')?.enable();
      this.getRmosByRscId(event.value);
    }
    else{
      this.inputForm.get('rsc')?.reset();
      this.inputForm.get('rsc')?.disable();
    }    
  }

  onRmoChanged(event: MatSelectChange){
    if(event.value!=null)
    {
      this.inputForm.get('wss')?.enable();
      this.getWssesByRmoId(event.value);
    }
    else{
      this.inputForm.get('wss')?.reset();
      this.inputForm.get('wss')?.disable();
    }  
    
  }

  onLandTypeChanged(event: MatSelectChange){
    console.log(event.value);
    if( event.source.triggerValue =='Water Intake'){
      this.inputForm.get('subCategory')?.enable();
      this.getLandSubCategories();
    }
    else{
      this.inputForm.get('subCategory')?.reset();
      this.inputForm.get('subCategory')?.disable();
    }
  }

  onLegalIssueChanged(event: MatSelectChange){
    
    if(event.value== 0)
    {
      this.isLegal = true;
    }
    else{
      this.isLegal =false;
    }
  }

  getRscs(){
    this.subscriber = this.rscService.getRscDetails().subscribe({
      next:data => {
        this.rscs = data;
      }
    })
  }

  getRmosByRscId( rscId: string){
    this.subscriber = this.rmoService.getRmoDetailsByRscId(rscId).subscribe({
      next:data => {
        this.rmos = data;
      }
    })
  }

  getWssesByRmoId( rmoId: string){
    this.subscriber = this.wssService.getWssDetailsByRmoId(rmoId).subscribe({
      next:data => {
        this.wsses = data;
      }
    })
  }

  getDsDivisionByDistrictId( districtId: string){
    this.subscriber = this.dsDivisionService.getDsDivisionDetailsByDistrictId(districtId).subscribe({
      next:data => {
        this.dsDivisions = data;
      }
    })
  }

  getGvDivisionByDsDivisionId( dsDivisionId: string){
    this.subscriber = this.gnDivisionService.getGsDivisionDetailsByDsDivisionId(dsDivisionId).subscribe({
      next:data => {
        this.gvDivisions = data;
      }
    })
  }

  getDistrictsByProviceId(provinceId : string) {
    this.subscriber = this.districtService.getDistrictDetailsByProvinceId(provinceId).subscribe({
      next: data => {
        this.districts = data;
      },
    });
  }

  getProvinces(){
    this.subscriber = this.provinceService.getProvinceDetails().subscribe({
      next: data => {
        this.provinces = data;
      },
    });
  }

  getLandTypes(){
    this.subscriber = this.landTypeService.getLandTypeDetails().subscribe({
      next: data => {
        this.landTypes = data;
      },
    });
  }

  getOwnerShips(){
    this.subscriber = this.ownerShipService.getOwnerShipDetails().subscribe({
      next: data => {
        this.ownerShips = data;
      },
    });
  }

  getLandSubCategories(){
    this.subscriber = this.landSubCategoryService.getSubCategoryDetails().subscribe({
      next: data => {
        this.categories = data;
      },
    });
  }

  ngOnDestroy() {
    this.subscriber.unsubscribe();
  }

  updateLand() {
    this.markFormControlsAsTouched();
    if (this.inputForm.valid) {
      console.log(this.inputForm.value)
      const landData = this.MapToLand();
      this.subscriber = this.landService.modifyLand(landData)
        .subscribe({
          next: () => {
            this.snackBarNotification.show(
              'success',
              'land Creation completed'
            );
            this.showDialog = false;
            this.router.navigate(['/land'], {
              state: {
                success: true,
                successMessage: 'land Creation completed'
              },
            });
          },
        });
    }
  }

}
