import { BaseEntity } from "../base-entity";
import { UserTypeStatus } from "./user-type-status";

export interface UserType extends BaseEntity{
    id: string;
    name: string;
    userTypeStatus: UserTypeStatus;
}