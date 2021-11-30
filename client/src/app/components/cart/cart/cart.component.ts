import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Cart } from 'src/app/_models/cart';
import { CartService } from 'src/app/_services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  myCart : Cart
  totalCost : number = 0
  constructor(private cartSerivce : CartService) { }

  ngOnInit(): void {
    this.cartSerivce.getCart().subscribe(x => {
      if(x != null){
        this.myCart = x;
        for(let item of this.myCart.items) this.totalCost += item.price;
      }
    });
  }

  deleteItemFromCart(id : number){
    this.myCart.items.filter(x=> x.id !== id)
    this.totalCost -= this.myCart.items.find(x => x.id == id).price;
    }
}
