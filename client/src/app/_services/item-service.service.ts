import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from '../_models/item';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
  })
}

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  getItems(){
    return this.http.get<Item[]>(this.baseUrl + 'items',httpOptions);
  }

  getItem(id : number){
    return this.http.get<Item>(this.baseUrl + "items/" + id,httpOptions);
  }
}