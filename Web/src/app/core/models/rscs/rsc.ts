import { BaseEntity } from "../base-entity";
import { RscStatus } from "./rsc-status";

export interface Rsc extends BaseEntity{
    id: string;
    rmoNumber: string;
    name: string;
    address: string;
    mobile: string;
    rscStatus: RscStatus;
}