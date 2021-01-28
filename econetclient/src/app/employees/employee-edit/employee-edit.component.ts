import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgxGalleryThumbnailsComponent } from '@kolkov/ngx-gallery';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Employee } from 'src/app/_models/employee';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { EmployeesService } from 'src/app/_services/employees.service';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.css']
})
export class EmployeeEditComponent implements OnInit {
// get access to our template form(in html) so as we can use it here
  @ViewChild('editForm') editForm: NgForm;
  employee: Employee;
  user: User;//logged on user
  //access browser events, so if user closes browser window before saving
  //there is no styling for this event it is what it is!
  @HostListener('window: beforeunload',['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private employeeService: EmployeesService, 
    private accountService: AccountService,
    private toastr: ToastrService) {
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => 
        {
          console.log(user);
          this.user = user
        });
   }

  ngOnInit(): void {
    this.loadEmployee();
  }

  loadEmployee() {
    this.employeeService.getEmployee(this.user.username).subscribe(employee =>
      {
        this.employee = employee;
        console.log('employee is ' + employee);
      });
  }

  updateEmployee() {
    this.employeeService.updateEmployee(this.employee).subscribe(employee => {
      this.toastr.success('Your profile was updated successfully');
      //reset the form and keep our values in view for the user
      this.editForm.reset(this.employee); // updates the dirty status to clean
    });
    
  }

}
