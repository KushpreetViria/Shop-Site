import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';
import { CartService } from 'src/app/_services/cart.service';

@Component({
  selector: 'app-item-card',
  templateUrl: './item-card.component.html',
  styleUrls: ['./item-card.component.css']
})
export class ItemCardComponent implements OnInit {
  @Input() item : Item;
  constructor(private cartService:CartService, private toastrService: ToastrService) { }
  
  ngOnInit(): void {
  }
  
  addToCart(){
    this.cartService.addItemToCart(this.item.id).subscribe((x) => {
      this.toastrService.info("Item added to cart: " + this.item.name);
    });
  }
  
}
