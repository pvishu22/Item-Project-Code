import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmitterService } from './Services/emmiter.service';
import { ItemService } from './Services/item.service';

@Component({    
    templateUrl: './itemdetail.component.html',
})
export class ItemDetailComponent {
    ItemData: any;
    isImageLoading: boolean;
    imageToShow: any;

    constructor(private http: HttpClient,private router: Router, private emitterService: EmitterService, private ItemService: ItemService) {        
        this.ItemData = emitterService.ItemData;
        this.ItemService.getImage(this.ItemData.Id).subscribe((data) => {
            this.createImageFromBlob(data);
            this.isImageLoading = false;
        }, (err) => {
            this.isImageLoading = false;
            console.log(err);
        })        
    }

    createImageFromBlob(image: Blob) {       
        let reader = new FileReader();
        reader.addEventListener("load", () => {
           this.imageToShow = reader.result;
        }, false);
     
        if (image) {
           reader.readAsDataURL(image);
        }
       }

    backtolist() {        
        this.router.navigate(['/ItemList']);
    }
}
