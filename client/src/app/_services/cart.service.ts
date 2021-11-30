import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cart } from '../_models/cart';

@Injectable({
  providedIn: 'root'
})

export class CartService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCart() : Observable<Cart>{
    var username = JSON.parse(localStorage.getItem('user'))["username"];
    return this.http.get<Cart>(this.baseUrl + `users/${username}/cart`)
  }

  addItemToCart(itemID:number){
    var username = JSON.parse(localStorage.getItem('user'))["username"];
    return this.http.put(this.baseUrl + `users/${username}/cart`,{},
    { params:{ id:itemID } }
    );
  };

  removeItemFromCart(itemID:number){
    var username = JSON.parse(localStorage.getItem('user'))["username"];
    return this.http.delete(this.baseUrl + `users/${username}/cart`, 
    { params:{ id:itemID } }
    );
  };
}
