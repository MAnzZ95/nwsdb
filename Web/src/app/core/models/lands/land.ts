import { BaseEntity } from "../base-entity";
import { LandAprovalStatus } from "./land-approval-status";
import { LandStatus } from "./land-status";

export interface Land extends BaseEntity{
    id: string;
    landTypeId: string;
    ownerShipId: string;
    provinceId: string;
    districId: string;
    dSDivisionId: string;
    gSDivisionId: string;
    subCategoryId: string;
    wssId: string;
    rscId: string;
    rmoId: string;
    landName: string;
    phone: string
    landArea: string;
    address: string;
    latitude: string;
    longitude: string;
    landStatus: LandStatus;
    landAprovalStatus: LandAprovalStatus;
    isLegal: boolean;
    remark: string;
}