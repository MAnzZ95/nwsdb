import { BaseEntity } from "../base-entity";

export interface OwnerShip extends BaseEntity{
    id: string;
    owner: string;
    deed: string;
}