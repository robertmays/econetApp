import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {
  error: any;

  constructor(private router: Router) { 
    //you can only access the router state inside the constructor
    //it is NOT available in the ngOnInit
    const navigation = this.router.getCurrentNavigation();
    //we only get access to the navigation on the first request
    //if the user refreshes the page we loose it so play safe ??
    //optional chaining by using the ???
    this.error = navigation?.extras?.state?.error; 

  }

  ngOnInit(): void {
  }

}
