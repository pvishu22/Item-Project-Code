import { Routes } from "@angular/router";
import { ItemDetailComponent } from "./itemdetail.component";
import { ItemlistComponent } from "./itemlist.component";

export const appRoutes: Routes = [
    { path: '', redirectTo: 'ItemList', pathMatch: 'full' }, 
    { path: 'ItemList', component: ItemlistComponent }, 
    { path: 'ItemDetail', component: ItemDetailComponent },
    { path: '**', component: ItemDetailComponent }
];