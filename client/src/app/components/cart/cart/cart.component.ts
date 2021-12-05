import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { delay, map,  } from 'rxjs/operators';
import { Cart } from 'src/app/_models/cart';
import { Item } from 'src/app/_models/item';
import { CartService } from 'src/app/_services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  myCart : Observable<Cart>
  //totalCost : number = 0
  constructor(private cartSerivce : CartService) { }

  ngOnInit(): void {
    this.myCart = this.cartSerivce.getCart();
  }
}
