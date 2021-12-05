import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';
import { CartService } from 'src/app/_services/cart.service';
import { ItemService } from 'src/app/_services/item.service';

@Component({
  selector: 'app-item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.css']
})
export class ItemDetailsComponent implements OnInit {
  item : Item;

  constructor(private itemService : ItemService, private cartService :CartService,
    private toastrService: ToastrService,private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.loadItem();
  }

  loadItem(){
    var id = parseInt(this.route.snapshot.paramMap.get('id'));
    if(!isNaN(id)){
      this.itemService.getItem(id).subscribe(item => {
        this.item = item;
      });
    }
  }

  addToCart(){
    this.cartService.addItemToCart(this.item).subscribe((x) => {
      this.toastrService.info("Item added to cart: " + this.item.name);
    });
  }
}
