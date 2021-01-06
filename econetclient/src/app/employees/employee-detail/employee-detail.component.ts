import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Employee } from 'src/app/_models/employee';
import { EmployeesService } from 'src/app/_services/employees.service';

@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {
  employee: Employee;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private employeeService: EmployeesService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    // this is a synchronus operation thing will happen one after the other (but no waiting for things to complete)
    //remember Rob your html page will load before this completes
    //so any interpolation on the html page will be undefind
    //so add *ngIf="employee" to your html tags to make sure our employee is available
    this.loadEmployee();

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ];    
  }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photo of this.employee.photos) {
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      });
    }

    return imageUrls;
  }

  loadEmployee() {
    this.employeeService.getEmployee(this.route.snapshot.paramMap.get('username')).subscribe(employee => {
      console.log(employee);
      this.employee = employee;
      //now i got the employee i can set the property below
      this.galleryImages = this.getImages();
    });
  }

}
