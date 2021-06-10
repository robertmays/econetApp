import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Employee } from '../_models/employee';

//services are singletons, so when a component create a service it stays alive until the application is closed
//so services are a good candidate for storing application state
// so we try and get employees from the service first else make http api call 

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  baseUrl = environment.apiUrl;
  employees: Employee[] = [];// store employee data in this service

  constructor(private http: HttpClient) { }

  getEmployees() {
    if (this.employees.length > 0){
      console.log('from service state:' + this.employees[0].username);
      return of(this.employees);
      
    } 
    return this.http.get<Employee[]>(this.baseUrl + 'users').pipe(
      map(employees => {       
        this.employees = employees;
        console.log('from service http call:' + this.employees[0].username);
        return employees;
      })
    );
  }

  getEmployee(username: string) {
    const employee = this.employees.find(e => e.username === username);
    if (employee !== undefined) return of(employee);
    return this.http.get<Employee>(this.baseUrl + 'users/' + username);
  }

  updateEmployee(employee: Employee) {
    //update our employee in this service otherwise they will see old data
    //update employee does not return anything so need to pipe into our stored employee data
    return this.http.put(this.baseUrl + 'users', employee).pipe(
      map(() => {
        const index = this.employees.indexOf(employee);
        this.employees[index] = employee;
      })
    );
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo' + photoId, {});
  }

  deletePhoto(photoId: Number){
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }
}
