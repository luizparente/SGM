import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServicesComponent } from './components/services/services.component';
import { ServicesTableComponent } from './components/services-table/services-table.component';
import {MatButtonModule} from '@angular/material/button'; 
import {MatTabsModule} from '@angular/material/tabs'; 
import {MatTableModule} from '@angular/material/table'; 
import {MatPaginatorModule} from '@angular/material/paginator'; 
import {MatSidenavModule} from '@angular/material/sidenav'; 
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { ViewServiceComponent } from './components/view-service/view-service.component';
import { ScheduleServiceComponent } from './components/schedule-service/schedule-service.component'; 
import {MatDividerModule} from '@angular/material/divider'; 
import {MatSelectModule} from '@angular/material/select'; 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatDatepickerModule} from '@angular/material/datepicker'; 
import {MatInputModule} from '@angular/material/input'; 

@NgModule({
  declarations: [
    ServicesComponent,
    ServicesTableComponent,
    ViewServiceComponent,
    ScheduleServiceComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatTabsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSidenavModule,
    MatSnackBarModule,
    MatDividerModule,
    MatSelectModule,
    MatDatepickerModule,
    MatInputModule
  ]
})
export class FieldManagerModule { }
