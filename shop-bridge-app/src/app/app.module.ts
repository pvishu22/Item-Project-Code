import { HttpClient, HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { Router } from "@angular/router";
import { AppComponent } from "./app.component";
import { AppRouteModule } from "./app.route.module";
import { ItemDetailComponent } from "./itemdetail.component";
import { ItemlistComponent } from './itemlist.component'
import { EmitterService } from "./Services/emmiter.service";
import { ItemService } from "./Services/item.service";

@NgModule({
    imports: [BrowserModule,FormsModule,ReactiveFormsModule,AppRouteModule, HttpClientModule], 
    declarations: [ItemlistComponent,ItemDetailComponent,AppComponent],
    bootstrap: [AppComponent],
    providers: [EmitterService,ItemService]
})
export class AppModule {

 } 