import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Item } from 'src/app/_models/item';
import { ItemService } from 'src/app/_services/item.service';
import { UserDetailsService } from 'src/app/_services/user-details.service';

@Component({
  selector: 'app-my-items',
  templateUrl: './my-items.component.html',
  styleUrls: ['./my-items.component.css']
})
export class MyItemsComponent implements OnInit {
  myItems$: Observable<Item[]>;

  constructor(private userService : UserDetailsService) { }

  ngOnInit(): void {
    this.loadMyItems();
  }

  loadMyItems(){
    this.myItems$ = this.userService.getUserItems();
  }

}
