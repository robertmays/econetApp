import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({  
  providedIn: 'root'
})
export class AccountService {
  //singleton while application lifetime, so data here live while client in the browser
  //whereas components are destroyed as soon as they go out of use
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  login(model: any){
    return this.http.post(this.baseUrl + 'account/login', model);
  }
}
