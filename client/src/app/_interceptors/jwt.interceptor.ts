import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { UserSession } from '../_models/user_session';
import { take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUserSession : UserSession;
    this.accountService.currentUser$.pipe(take(1)).subscribe(x => currentUserSession = x);
    if(currentUserSession){
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUserSession.token}`
        }
      });
    }
    return next.handle(request);
  }
}
