import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserDetails } from '../_models/user_details';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
  })
}

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {
  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  getUsers() : Observable<UserDetails[]> {
    return this.http.get<UserDetails[]>(this.baseUrl + 'users',httpOptions);
  }

  getUser(username : string){
    return this.http.get<UserDetails>(this.baseUrl + 'users/' + username,httpOptions);
  }

  getUserCart(username : string){
    return this.http.get<UserDetails>(this.baseUrl+'users/'+username+'/cart',httpOptions);
  }

  getUserOrders(username: string){
    return this.http.get<UserDetails[]>(this.baseUrl+'users/'+username+'/orders',httpOptions);
  }
}
