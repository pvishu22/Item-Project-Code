import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { item } from './models/item';
import { EmitterService } from './Services/emmiter.service';
import { ItemService } from './Services/item.service';

@Component({
  selector: 'app-itemlist',
  templateUrl: './itemlist.component.html',
})
export class ItemlistComponent implements OnInit {

  additem: FormGroup;
  items: Array<item>;
  item: any;
  fileToUpload: File = null;
  data: item;
  npattern = "^[0-9]*$";

  ngOnInit() {
    this.additem = new FormGroup({
      'Name': new FormControl(null, Validators.required)      
    });
  }

  constructor(private http: HttpClient, private router: Router, private emitterService: EmitterService, private ItemService: ItemService) {
    debugger;
    this.ItemService.getAllItem().subscribe((ItemList: Array<item>) => {      
      this.items = ItemList;      
    }, (err) => {
      console.log(err);
    })
  }

  HandleFileInput(files: FileList) {    
    this.fileToUpload = files.item(0);
  }

    onClickSubmit(additem: NgForm) {
   
    this.data = additem.value;

    this.ItemService.addItem(this.data).subscribe((newData: item) => {
      
      if(this.fileToUpload != null)
      {
        this.ItemService.AddItemImage(this.fileToUpload,newData.Id).subscribe((New_data: item) => {
          this.items.push(New_data);          
        }, error => {
          console.log(error);
        });
      }   
      alert('Item Added Successfully');
    }, (err) => {
      console.log(err);
    }); 
  }

  deleteitem(id: number) {    
    this.ItemService.deleteItemById(id).subscribe((item: item) => {
      if (item != null) {
        this.items.forEach((value, index) => {
          if (value.Id == id) this.items.splice(index, 1);
        });
        alert(`Item Deleted Successfully`)
      }
      else
        alert('Item Not Found');
    }, (err) => {
      console.log(err);
    })
  }

  viewitem(id: number) {    
    this.ItemService.getItemById(id).subscribe((item: item) => {
      if (item != null) {
        this.item = this.items.find(obj => {
          return obj.Id == id
        });
        this.router.navigate(['/ItemDetail']);
        this.emitterService.publish(this.item);
      }
      else {
        alert('Item Not Found');
      }
    }, (err) => {
      console.log(err);
    })
  }

}
