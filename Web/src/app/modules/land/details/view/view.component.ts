import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
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

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.scss'],
  styles: [
    `
      .disable-button {
        background-color: #7c7d7d !important;
        color: white;
      }
    `,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LandViewComponent implements OnInit {

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
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.getProvinces();
    this.getRscs();
    this.getLandTypes();
    this.getOwnerShips();
    this.getDistricts();
    this.getDsDivisions();
    this.getGvDivisions();
    this.getRmosByRscId();
    this.getWssesByRmoId();
    this.getLandSubCategories();

    this.inputForm = this.inputFormBuilder.group({
      landName:'',
      phone:'',
      area:'',
      address:'',
      province:'',
      district:'',
      dsDivision:'',
      gvDivision:'',
      ownerShip:'',
      subCategory:'',
      rsc:'',
      rmo:'',
      wss:'',
      landType:'',
      legalStatus:'',
      remark:'',
      longitude: '',
      latitude:''
    });

    this.getCompanyDetails();   
    this.inputForm.disable();
  }

  private getIdFromUrl(): string | null {
    const urlParts = window.location.href.split('/');
    const landIdIndex = urlParts.indexOf('land') + 1;
    if (urlParts.length > landIdIndex) {
      return urlParts[landIdIndex];
    }
    return null;
  }

  private getLandIdFromRouting(): string {
    const id: string = this.route.snapshot.params['id'];
    return id;
  }


  getCompanyDetails() {
    const id =this.getLandIdFromRouting() ?? '';
    const sub = this.landService.getLandDetailsById(id).subscribe({
      next: response => {
        this.data = response;
        if (this.data !== null) {
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
    }
    else{
      this.inputForm.get('districts')?.reset();
      this.inputForm.get('districts')?.disable();
    }    
  }

  getStatusClass(status: number): string {
    if (status == 0) {
      return 'status-available';
    } else if (status == 1) {
      return 'status-not-available';
    } else if (status == 2) {
      return 'status-delete';
    } else {
      return '';
    }
  }

  onDistrictChanged(event: MatSelectChange) {
    if(event.value!=null)
    {
      this.inputForm.get('dsDivisions')?.enable();
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

  getRmosByRscId(){
    this.subscriber = this.rmoService.getRmoDetails().subscribe({
      next:data => {
        this.rmos = data;
      }
    })
  }

  getWssesByRmoId(){
    this.subscriber = this.wssService.getWssDetails().subscribe({
      next:data => {
        this.wsses = data;
      }
    })
  }

  getDsDivisions(){
    this.subscriber = this.dsDivisionService.getDSDivisionDetails().subscribe({
      next:data => {
        this.dsDivisions = data;
      }
    })
  }

  getGvDivisions(){
    this.subscriber = this.gnDivisionService.getGsDivisionDetails().subscribe({
      next:data => {
        this.gvDivisions = data;
      }
    })
  }

  getDistricts() {
    this.subscriber = this.districtService.getDistrictDetails().subscribe({
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
}
