import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { catchError } from 'rxjs/operators'
import { item } from "../models/item";

@Injectable()
export class ItemService {

    baseUrl:string;

    constructor(private http: HttpClient) {
        this.baseUrl = "http://localhost:49784";
    }

    getAllItem(): Observable<item[]> {        
        return this.http.get<item[]>(this.baseUrl + '/Api/Item/ItemDetails')
            .pipe(catchError(this.handleError('getAllItem', [])))
    }

    getImage(Id:number): Observable<Blob> {       
        return this.http.get(this.baseUrl + `/Api/Item/GetImage/${Id}`, { responseType: 'blob' });
    }

    getItemById(id: number): Observable<item> {
        return this.http.get<item>(this.baseUrl + `/Api/Item/GetItemDetailsById/${id}`)
            .pipe(catchError(this.handleError('getItemById', null)))
    }

    deleteItemById(id: number): Observable<item> {
        return this.http.delete<item>(this.baseUrl + `/Api/Item/DeleteItem/${id}`)
            .pipe(catchError(this.handleError('deleteItemById', null)))
    }


    addItem(itemObj: item): Observable<item> {
        return this.http.post<item>(this.baseUrl + '/Api/Item/InsertItemDetails',
            JSON.stringify(itemObj),
            {
                headers: new HttpHeaders({ 'Content-Type': 'application/json' })
            })
            .pipe(catchError(this.handleError<item>('addItem')))       
    }

    AddItemImage(fileToUpload: File, Id: number): Observable<item> {        
        let headers = new HttpHeaders();

        headers.append('Content-Type', 'application/json');
        const httpOptions = { headers: headers };
        const formData: FormData = new FormData();
        formData.append('Image', fileToUpload, fileToUpload.name);
        formData.append('Id', Id.toString());
        return this.http.post<item>('http://localhost:49784/Api/Item/UploadImage', formData, httpOptions);
    }

    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(error);
            return of(result as T);
        };
    }
}