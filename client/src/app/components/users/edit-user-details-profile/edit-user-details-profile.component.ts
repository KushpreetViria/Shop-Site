import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserDetails } from 'src/app/_models/user_details';
import { UserDetailsService } from 'src/app/_services/user-details.service';

@Component({
  selector: 'app-edit-user-details',
  templateUrl: './edit-user-details-profile.component.html',
  styleUrls: ['./edit-user-details-profile.component.css']
})
export class EditUserDetailsProfileComponent implements OnInit {
  @ViewChild('editForm') editForm : NgForm;
  @HostListener('window:beforeunload',['$event']) 
    unloadNotification($event: any){
      if(this.editForm.dirty){
        $event.returnValue = true;
      }
    }
  user : UserDetails

  constructor(private userDetailService: UserDetailsService, private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser(){
    this.userDetailService.getUser().subscribe(user =>{
      this.user = user;
    });
  }

  updateUserDetails(){
    this.userDetailService.updateUser(this.user).subscribe(() =>{
      this.toastrService.success("Profile updated!");
      this.editForm.reset(this.user);
    });
  }
}
