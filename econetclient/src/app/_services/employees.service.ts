import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Employee } from '../_models/employee';


@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getEmployees() {
    return this.http.get<Employee[]>(this.baseUrl + 'users');
  }

  getEmployee(username: string) {
    return this.http.get<Employee>(this.baseUrl + 'users/' + username);
  }

  updateEmployee(employee: Employee) {
    return this.http.put(this.baseUrl + 'users', employee);
  }
}
