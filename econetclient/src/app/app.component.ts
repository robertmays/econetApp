import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Econet';
  users: any;// no type safety

  constructor(private http: HttpClient) {
    //ngx bootstrap is better for angular as it tracks notification changes
    // bootstrap uses jquery so that can muddle the picture as it manipulates the DOM too!!!!
  }

  ngOnInit() {
    this.getUsers();   
  }

  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    });
  }
}
