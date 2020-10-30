import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({  
  providedIn: 'root'
})
export class AccountService {
  //singleton while application lifetime, so data here live while client in the browser
  //whereas components are destroyed as soon as they go out of use
  baseUrl = 'https://localhost:5001/api/';
  //ReplaySubject like a buffer to store values sets up as an observable so out other components can subscribe
  //to it. especially or _auth guard so it can see ig a user changes
  //(1) store one previous value
  private currentUserSource = new ReplaySubject<User>(1);
  //$ suffix means use as an observable
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any){
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          //next(user) next value to store
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(user);
        }
        //return user; as i am not returning anything i will get an undefined, thats ok as dont need to return anything here anyway
      })
    );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
