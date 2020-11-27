import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/_models/employee';
import { EmployeesService } from 'src/app/_services/employees.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[];

  constructor(private employeeService: EmployeesService) { }

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees() {
    this.employeeService.getEmployees().subscribe(employees => {
      //dont need error handling as our interceptors take care of it.
      this.employees = employees;
    });
  }

}
