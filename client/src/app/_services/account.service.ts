import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { pipe, ReplaySubject } from 'rxjs';
import {map, take} from 'rxjs/operators'
import { environment } from 'src/environments/environment';
import { UserSession } from '../_models/user_session';

//injectable into components,
//services are singleton that last the app/component cycle
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<UserSession>(1);
  currentUser$ = this.currentUserSource.asObservable();
  
  constructor(private http:HttpClient) { }
  
  login(model: any){
    return this.http.post<UserSession>(this.baseUrl + 'account/login',model).pipe(
      map((response: UserSession) => {
        const user = response;
        if (user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user)
        }
      })
    );
  }
    
  register(model: any){
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user:UserSession) => {
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }
    
    logout() {
      localStorage.removeItem('user');
      this.currentUserSource.next(null);
    }
    
    setCurrentUser(user : UserSession){
      this.currentUserSource.next(user);
  }
}
    