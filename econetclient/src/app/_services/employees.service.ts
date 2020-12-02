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

  getEmployee(id: string) {
    return this.http.get<Employee>(this.baseUrl + 'users/' + id);
  }
}
