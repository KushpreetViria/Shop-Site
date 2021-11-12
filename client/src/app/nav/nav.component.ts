import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  
  constructor(public accountService: AccountService, private router: Router,
    private toastr: ToastrService) {}
    
    ngOnInit(): void {
      console.log("[D] Init of nav component")
    }
    
    login(){
      this.accountService.login(this.model).subscribe(response =>{
        console.log(response);
        this.router.navigateByUrl('/items');
      }, err => {
        this.toastr.error(err.error);
        console.log("[D] while logging in: ", err.errror);
      }); 
    }
    
    logout(){
      this.accountService.logout();
      this.router.navigateByUrl('/');
      console.log("[D] logging out...");
    }
  }
  