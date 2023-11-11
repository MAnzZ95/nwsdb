import { BaseEntity } from "../base-entity";
import { RmoStatus } from "./remo-status";

export interface Rmo extends BaseEntity{
    id: string;
    rmoNumber: string;
    name: string;
    address: string;
    mobile: string;
    rmoStatus: RmoStatus;
}