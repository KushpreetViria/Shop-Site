import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  AppTitle = 'My App';
  users: any;
  
  //private property called http of type HttpClient
  constructor(private http: HttpClient) {
    this.users = "";
  }
  
  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(): void {
    this.http.get("https://localhost:5001/api/users").subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    });
  }
}