import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CustomerListComponent } from './customers/customer-list/customer-list.component'
import { CustomerDetailComponent } from './customers/customer-detail/customer-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { RegisterComponent } from './register/register.component';
import { SearchComponent } from './search/search.component';
import { AuthGuard } from './_guards/auth.guard';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { EmployeeListComponent } from './employees/employee-list/employee-list.component';
import { EmployeeDetailComponent } from './employees/employee-detail/employee-detail.component';
import { EmployeeEditComponent } from './employees/employee-edit/employee-edit.component';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

//angular can get confused in routing between 'employees/:username' and 'employees/edit' as soon as it sees employees/
//it will take you to the first one in you route 'employees/:username' hence i am using 'employee/edit' with no 's'
// you can also use pathMatch: 'full' then 'employees/:username' and 'employees/edit' would work
//Angular use first match win policy
const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'employees', component: EmployeeListComponent },
      { path: 'employees/:username', component: EmployeeDetailComponent },
      { path: 'employee/edit', component: EmployeeEditComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      { path: 'customers', component: CustomerListComponent },
      { path: 'customers/:id', component: CustomerDetailComponent },
      { path: 'lists', component: ListsComponent },
      { path: 'search', component: SearchComponent },
      { path: 'messages', component: MessagesComponent }
    ]
  }, 
  { path: 'errors', component: TestErrorsComponent},
  { path: 'register', component: RegisterComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
