import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Item } from 'src/app/_models/item';
import { CartService } from 'src/app/_services/cart.service';

@Component({
  selector: 'app-cart-card',
  templateUrl: './cart-card.component.html',
  styleUrls: ['./cart-card.component.css']
})
export class CartCardComponent implements OnInit {
  @Input() item : Item;
  @Output() removeCartItem = new EventEmitter();
  hideMe : boolean //how to actually delete dynamically created component?

  constructor(private cartService : CartService) { }

  ngOnInit(): void {
    this.hideMe = false;
  }

  removeItem() {
    this.cartService.removeItemFromCart(this.item.id).subscribe(x => {
    this.removeCartItem.emit(this.item.id);
    });
    this.hideMe = true;
  }

}
