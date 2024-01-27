import { BaseEntity } from "../base-entity";
import { WssStatus } from "./wss-status";

export interface Wss extends BaseEntity{
    id: string;
    wssNumber: string;
    name: string;
    address: string;
    mobile: string;
    wssStatus: WssStatus;
}