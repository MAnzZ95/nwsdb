import { BaseEntity } from "../base-entity";
import { userStatus } from "./user-status";

export interface User extends BaseEntity{
    id:string;
    name : string;
    position : string;
    email : string;
    mobile : string;
    address: string;
    password :string;   
    gender : string;
    userStatus :userStatus; 
}