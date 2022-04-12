import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyServicesComponent } from './components/my-services/my-services.component';
import {MatRadioModule} from '@angular/material/radio'; 
import { GoogleMapsModule } from '@angular/google-maps';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { ServiceMapComponent } from './components/service-map/service-map.component';
import {MatButtonModule} from '@angular/material/button'; 
import {MatSidenavModule} from '@angular/material/sidenav'; 
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { ViewServiceComponent } from './components/view-service/view-service.component';
import {MatDividerModule} from '@angular/material/divider'; 
import {MatFormFieldModule} from '@angular/material/form-field'; 
import {MatInputModule} from '@angular/material/input'; 
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    MyServicesComponent,
    ServiceMapComponent,
    ViewServiceComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatRadioModule,
    GoogleMapsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    MatButtonModule,
    MatSidenavModule,
    MatSnackBarModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule
  ]
})
export class FieldTechnicianModule { }
