import { Component, OnInit } from '@angular/core';
import { UserDetails } from 'src/app/_models/user_details';
import { UserDetailsService } from 'src/app/_services/user-details.service';

@Component({
  selector: 'app-user-lists',
  templateUrl: './user-lists.component.html',
  styleUrls: ['./user-lists.component.css']
})

//vid 104 @ 1:00
//do i need this? no i dont need to show a list of users in a shopping site
//might need to do this for items
export class UserListsComponent implements OnInit {
  users: UserDetails[]
  constructor(private userDetailService : UserDetailsService) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(){
    this.userDetailService.getUsers().subscribe(users => {
      this.users = users;
    })
  }
}
