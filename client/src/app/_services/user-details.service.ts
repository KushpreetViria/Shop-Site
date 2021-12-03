import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UserDetails } from '../_models/user_details';
import { AccountService } from './account.service';

//for updating / changing user profile info
@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient,private accountService:AccountService) {}

  getUser() {
    return this.http.get<UserDetails>(this.baseUrl + 'users/self');
  }

  //move this to its own service ...
  getUserTransactions(){
    return this.http.get<UserDetails[]>(this.baseUrl+'users/transactions');
  }
}
