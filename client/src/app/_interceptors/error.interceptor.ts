import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  
  constructor(private router: Router, private toastr: ToastrService) {}
  
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(err => {
        if(err) {
          switch(err.status){
            case 400:
            if(err.error.errors){ //error with an error parameter (multiple error responses)
              const modelStateErrors = [];
              for(const key in err.error.errors){
                modelStateErrors.push(err.error.errors[key]);
              }
              for(let i = 0; i < modelStateErrors.flat().length; i++){
                this.toastr.error(modelStateErrors.flat()[i])
              }
              console.log(err);
              throw modelStateErrors.flat(); //
            }else{ //regular 400 error 
              // ANGULAR always prints StatusText OK for >400  responses????
              if(err.error){
                this.toastr.error(err.error);
              }else if(err.status == 400){ //manually fix it for now
                this.toastr.error("Error: Bad Request");
              }else{
                this.toastr.error("Error: "+ err.statusText);
              }
            }
            break;
            case 401:
            this.toastr.error("Error: " + err.error);
            break;
            case 404:
            this.router.navigateByUrl('/404-not-found');
            break;
            case 500:
            const navigationExtras: NavigationExtras = {
              state: {error: err.error}
            };
            this.router.navigateByUrl('/500-server-error',navigationExtras);
            break;
            default:
              this.toastr.error(err.error);
              console.log(err);
            break;
          }
        }
        return throwError(err);;
      })
      )
    }
  }