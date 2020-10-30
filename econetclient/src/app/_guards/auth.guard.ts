import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})

//AuthGuard handles the subscription for you
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService, private toastr: ToastrService) {}

  canActivate(): Observable<boolean> {
    //pipe means we are going to do something with it, although currentUser$ is an observable we dont always need to subscribe to it. 
        return this.accountService.currentUser$.pipe(
          map(user => {
            if (user) return true;// return an observable of true
            this.toastr.error('You shall not pass the bridge!');
          })
        );
  }
  
}
