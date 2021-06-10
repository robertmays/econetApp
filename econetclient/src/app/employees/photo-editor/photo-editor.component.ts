import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { Employee } from 'src/app/_models/employee';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { EmployeesService } from 'src/app/_services/employees.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() employee: Employee;
  uploader: FileUploader;
  hasBaseDropZoneOver:boolean;
  baseUrl = environment.apiUrl;
  user: User;


  constructor(private accountService: AccountService, private employeeService: EmployeesService) {
    //get the current logged on user
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }

  setMainPhoto(photo: Photo) {
    this.employeeService.setMainPhoto(photo.id).subscribe(() => {
      this.user.photoUrl = photo.url;
      //update our observable and our photo in local storage
      this.accountService.setCurrentUser(this.user);
      this.employee.photoUrl = photo.url;
      this.employee.photos.forEach(p => {
        if (p.isMain) p.isMain = false;
        if (p.id === photo.id) p.isMain = true;
      });
    });
  }

  deletePhoto(photoId: number){
    this.employeeService.deletePhoto(photoId).subscribe(() => {
      //filter to get an array of all photos apart from the one we just deleted,
      //our interceptor is taking care of our errors so no need to add error handler here
      this.employee.photos = this.employee.photos.filter(x => x.id !== photoId);
    });
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo = JSON.parse(response);
        this.employee.photos.push(photo);
      }
    }
  }

  

}
