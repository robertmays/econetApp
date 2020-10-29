import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //on the parent html <register> tag it will use [] to send us data. when i need to send data to a parent the parent will use () to receive that data
  //@Input allows the parent component to pass data to this component(child)
  //@Output allows a child to send data to a parent
  //@Input() users: any;
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register() {
   this.accountService.register(this.model).subscribe(response => {
     console.log(response);
     this.cancel();
   }, error => {
     console.log(error);
   });
  }

  cancel() {
    console.log('Cancelled');
    this.cancelRegister.emit(false);
  }

}
