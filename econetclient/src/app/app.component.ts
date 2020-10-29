import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Econet';
  users: any;// no type safety

  constructor(private accountService: AccountService) {
    //ngx bootstrap is better for angular as it tracks notification changes
    // bootstrap uses jquery so that can muddle the picture as it manipulates the DOM too!!!!
  }

  ngOnInit() {
    
    this.setCurrentUser();  
  }

  setCurrentUser() {
    //json.parse to read a json.stringify back to json
    const user: User = JSON.parse(localStorage.getItem('user'));

    this.accountService.setCurrentUser(user);
  }

}
