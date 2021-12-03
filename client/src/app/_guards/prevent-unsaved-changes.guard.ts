import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { EditUserDetailsProfileComponent } from '../components/users/edit-user-details-profile/edit-user-details-profile.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(component: EditUserDetailsProfileComponent) : boolean{
    if(component.editForm.dirty){
      return confirm("You have unsaved changes. Are you sure you want to leave this page?")
    }
    return true;
  }
}
