import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-update-item',
  templateUrl: './update-item.component.html',
  styleUrls: ['./update-item.component.css']
})
export class UpdateItemComponent implements OnInit {

  constructor(private toastr : ToastrService) {
  }

  ngOnInit(): void {
    this.toastr.info("hey");
  }

}
