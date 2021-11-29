import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/_models/item';
import { ItemService} from 'src/app/_services/item-service.service';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {
  items: Item[];
  constructor(private itemService: ItemService) { }

  ngOnInit(): void {
    this.loadItems();
  }

  loadItems(){
    this.itemService.getItems().subscribe(items =>
      this.items = items);
  }

  toStringItem(item : Item) : string {
    return JSON.stringify(item);
  }

}
