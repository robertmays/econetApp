import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { EmployeeEditComponent } from '../employees/employee-edit/employee-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(component: EmployeeEditComponent):  boolean {
    if (component.editForm.dirty) {
      return confirm('Are you sure you want to continue? as any unsaved changes will be lost');
    }
    return true;
  }
  
}
