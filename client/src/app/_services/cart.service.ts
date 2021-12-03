import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cart } from '../_models/cart';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})

export class CartService {
  baseUrl = environment.apiUrl;
  cartUrl = "users/cart";
  
  constructor(private http: HttpClient,private accountService: AccountService) {}
  
  getCart() : Observable<Cart>{
    return this.http.get<Cart>(this.baseUrl + this.cartUrl)
  }
  
  addItemToCart(itemID:number){
    return this.http.put(this.baseUrl + this.cartUrl,{},
      { params:{ id:itemID } }
    );
  }
    
  removeItemFromCart(itemID:number){
    return this.http.delete(this.baseUrl + this.cartUrl, 
      { params:{ id:itemID } }
      );
    }
}
    