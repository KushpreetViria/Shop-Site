import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { pipe, ReplaySubject } from 'rxjs';
import {map, take} from 'rxjs/operators'
import { User } from '../_models/user';

//injectable into components,
//services are singleton that last the app/component cycle
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = "https://localhost:5001/api/"
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  username: String = "";
  
  constructor(private http:HttpClient) { }
  
  login(model: any){
    return this.http.post<User>(this.baseUrl + 'account/login',model).pipe(
      map((response: User) => {
        const user = response;
        if (user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user)
          this.setCurrentUser(user);
        }
      })
    );
  }
    
  register(model: any){
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user:User) => {
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
          this.setCurrentUser(user);
        }
      })
    );
  }
    
    logout() {
      localStorage.removeItem('user');
      this.currentUserSource.next(null);
      this.setCurrentUser(null);
    }
    
    setCurrentUser(user : User){
      this.currentUserSource.next(user);
      this.currentUser$.pipe(take(1)).subscribe((name: User) => {
        this.username = name ? name.username :  "";
        });
  }
}
    