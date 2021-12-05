import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cart } from '../_models/cart';
import { Item } from '../_models/item';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})

export class CartService {
  baseUrl = environment.apiUrl;
  cartUrl = "users/cart";

  private _myCart : Cart = null;
  
  constructor(private http: HttpClient,private accountService: AccountService) {}
  
  getCart() : Observable<Cart>{
    if(this._myCart !== null) return of(this._myCart);
    return this.http.get<Cart>(this.baseUrl + this.cartUrl).pipe(
      map(cart => {
        this._myCart = cart;
        return this._myCart;
      })
    )
  }
  
  addItemToCart(item:Item){
    return this.http.put(this.baseUrl + this.cartUrl,{},{ params:{ id: item.id } }).pipe(map(() => {
      if(this._myCart){
        this._myCart.count++;
        this._myCart.totalCost = (this._myCart.totalCost*100 + item.price*100)/100;
        this._myCart.items.push(item)
      }
    }));
  }
    
  removeItemFromCart(item:Item){
    return this.http.delete(this.baseUrl + this.cartUrl, { params:{ id:item.id } }).pipe(map(() => {
      if(this._myCart){
        this._myCart.totalCost = (this._myCart.totalCost*100 - item.price*100)/100;
        this._myCart.count--;
        this._myCart.items = this._myCart.items.filter(x => x.id !== item.id);
      }
    }));
    }
}
    