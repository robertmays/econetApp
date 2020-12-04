import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Employee } from 'src/app/_models/employee';
import { EmployeesService } from 'src/app/_services/employees.service';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {
  employee: Employee;

  constructor(private employeeService: EmployeesService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    //remember Rob your html page will load before this completes
    //so any interpolation on the html page will be undefind
    //so add *ngIf="employee" to your html tags to make sure our employee is available
    this.loadEmployee();
  }

  loadEmployee() {
    this.employeeService.getEmployee(this.route.snapshot.paramMap.get('id')).subscribe(employee => {
      console.log(employee);
      this.employee = employee;
    });
  }

}
