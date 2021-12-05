import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Item } from '../_models/item';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  private _itemStore: Item[] = [];

  baseUrl = environment.apiUrl
  constructor(private http: HttpClient) { }

  getItems(){
    if(this._itemStore.length > 0) return of(this._itemStore)
    
    return this.http.get<Item[]>(this.baseUrl + 'items').pipe(
      map(items => {
        this._itemStore = items;
        return this._itemStore;
      })
    );
  }

  getItem(id : number){
    const item = this._itemStore.find(x => x.id == id);
    if(item !== undefined) return of(item);
    
    return this.http.get<Item>(this.baseUrl + "items/" + id).pipe(
      map(item => {
        this._itemStore.push(item);
        return item;
      })
    );
  }
}