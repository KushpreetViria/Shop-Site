import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Item } from 'src/app/_models/item';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  @Input() item : Item;
  constructor(private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  addToCart(){
    this.toastrService.info("Item added to cart: " + this.item.name);
  }

}
