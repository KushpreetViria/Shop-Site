import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  AppTitle = 'My App';
  users: any;
  
  //private property called http of type HttpClient
  constructor(private http: HttpClient, private accountService: AccountService) {
    //nothing...
  }
  
  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser();
  }

  getUsers(): void {
    this.http.get("https://localhost:5001/api/users").subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    });
  }

  setCurrentUser(){
    const user: User = JSON.parse(localStorage.getItem('user'));
     this.accountService.setCurrentUser(user);
  }
}

