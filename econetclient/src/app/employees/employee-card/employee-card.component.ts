import { Component, Input, OnInit } from '@angular/core';
import { Employee } from 'src/app/_models/employee';

@Component({
  selector: 'app-employee-card',
  templateUrl: './employee-card.component.html',
  styleUrls: ['./employee-card.component.css']
})
export class EmployeeCardComponent implements OnInit {
  //get employee from parent
  @Input() employee: Employee;

  constructor() { }

  ngOnInit(): void {
    console.log('card emp = ' + this.employee)
  }

}
