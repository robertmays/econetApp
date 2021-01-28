import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { BusyService } from '../_services/busy.service';
import { delay, finalize } from 'rxjs/operators';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
   // when sending a request call our busy method
    this.busyService.busy();

    //once the request comes back we can turn of our busy loading indicator 
    return next.handle(request).pipe(
      //add a fake delay
      delay(1000),
      finalize(() => {
        //do stuff when things have completed
        this.busyService.idle();
      })
    )
  }
}
