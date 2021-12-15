import { Component, OnInit } from '@angular/core';
import { UserDetails } from 'src/app/_models/user_details';
import { UserDetailsService } from 'src/app/_services/user-details.service';

@Component({
  selector: 'app-user-details-profile',
  templateUrl: './user-details-profile.component.html',
  styleUrls: ['./user-details-profile.component.css']
})
export class UserDetailsProfileComponent implements OnInit {
  user : UserDetails;
  missingDetails : boolean = false;
  fullAddress : string;

  constructor(private userDetailService : UserDetailsService) { }

  ngOnInit(): void {
    this.loadUserProfile();

  }

  loadUserProfile(){
    this.userDetailService.getUser().subscribe(user =>{
      this.user = user;

      // checks if one of the editable profile properties is empty
      for(const property in this.user){
        if(!(property === 'id' ||
            property === 'username' ||
            property === 'cart' ||
            property === 'items' ||
            property === 'transactions') && !this.user[property]){
              this.missingDetails = true;
              break;
        }
      }
    })
  }

}
