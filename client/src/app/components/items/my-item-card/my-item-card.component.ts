import { Component, Input, OnInit } from '@angular/core';
import { Item } from 'src/app/_models/item';

@Component({
  selector: 'app-my-item-card',
  templateUrl: './my-item-card.component.html',
  styleUrls: ['./my-item-card.component.css']
})
export class MyItemCardComponent implements OnInit {
  @Input() item : Item;

  constructor() { }

  ngOnInit(): void {
  }

}
