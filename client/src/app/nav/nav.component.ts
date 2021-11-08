import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  currentUser$: Observable<User>;
  username: String = "";

  constructor(private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;

    this.currentUser$.pipe(take(1)).subscribe((user: User) => {
      this.username = user.username;
    })
    console.log("[D] Init of nav component")
  }

  login(){
    this.accountService.login(this.model).subscribe(response =>{
      console.log(response);
      this.username = this.model.username;
    }, error => console.error());
    console.log("[D] logging in...")
  }

  logout(){
    this.accountService.logout();
    console.log("[D] logging out...");
  }

}
