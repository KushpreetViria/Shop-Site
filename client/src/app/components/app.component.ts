import { Component, OnInit } from '@angular/core';
import { UserSession } from '../_models/user_session';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  AppTitle = 'Shop Site';
  
  constructor(private accountService: AccountService) {
    //nothing...
  }
  
  ngOnInit(): void {
    this.setCurrentUser(); //
  }

  setCurrentUser(){
    const user: UserSession = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }
}

