import { Injectable } from "@angular/core";
import { item } from "../models/item";

@Injectable()
export class EmitterService{
   
    ItemData:item;

    constructor(){
    }

    publish(data:any){
        this.ItemData = data;    
    }
}