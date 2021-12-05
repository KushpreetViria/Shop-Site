import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UserDetails } from '../_models/user_details';
import { AccountService } from './account.service';

//for updating / changing user profile info
@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {
  baseUrl = environment.apiUrl;

  private _myUserDetails : UserDetails = null;

  constructor(private http: HttpClient,private accountService:AccountService) {}

  getUser() {
    if(this._myUserDetails !== null) return of(this._myUserDetails)
    return this.http.get<UserDetails>(this.baseUrl + 'users/self').pipe(map(user => {
      this._myUserDetails = user;
      return user;
    }));
  }

  updateUser(userDetails : UserDetails){
    return this.http.put(this.baseUrl+'users/self',userDetails).pipe(map( () => {
      this._myUserDetails = null;
    }));
  }

  //move this to its own service ...
  getUserTransactions(){
    return this.http.get<UserDetails[]>(this.baseUrl+'users/transactions');
  }
}
