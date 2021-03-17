import { Component } from "@angular/core";
import { ItemService } from "./Services/item.service";

@Component({
    selector: 'app-root',
    template: `<router-outlet></router-outlet>`,
    providers: [ItemService]
})
export class AppComponent {

    constructor(private ItemService: ItemService) {

    }
}