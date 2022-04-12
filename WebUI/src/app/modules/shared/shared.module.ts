import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/navbar/navbar.component';
import {MatDatepickerModule} from '@angular/material/datepicker'; 
import { MatNativeDateModule } from '@angular/material/core'; 
import {MatFormFieldModule} from '@angular/material/form-field'; 
import {MatInputModule} from '@angular/material/input'; 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { HomeCarouselComponent } from './components/home-carousel/home-carousel.component';
import { NewsComponent } from './components/news/news.component';
import { NewsElementComponent } from './components/news-element/news-element.component';
import { FooterComponent } from './components/footer/footer.component';
import { RouterModule } from '@angular/router'; 
import {MatButtonModule} from '@angular/material/button'; 
import {MatBottomSheetModule} from '@angular/material/bottom-sheet'; 
import {MatListModule} from '@angular/material/list'; 
import { BottomSheetLoginAs } from './components/bottom-sheet-login-as/bottom-sheet-login-as.component';
import { EventsComponent } from './components/events/events.component';
import { EventElementComponent } from './components/event-element/event-element.component';
import { PublicServicesComponent } from './components/public-services/public-services.component';
import { PublicServicesElementComponent } from './components/public-services-element/public-services-element.component';
import {MatTableModule} from '@angular/material/table'; 
import {MatPaginatorModule} from '@angular/material/paginator';
import { NotFoundComponent } from './components/not-found/not-found.component'; 

@NgModule({
  declarations: [
    NavbarComponent,
    LoginComponent,
    HomeComponent,
    HomeCarouselComponent,
    NewsComponent,
    NewsElementComponent,
    FooterComponent,
    BottomSheetLoginAs,
    EventsComponent,
    EventElementComponent,
    PublicServicesComponent,
    PublicServicesElementComponent,
    NotFoundComponent
  ],
  imports: [
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatBottomSheetModule,
    MatListModule,
    MatTableModule,
    MatPaginatorModule,
  ],
  exports: [
    NavbarComponent,
    FooterComponent
  ]
})
export class SharedModule { }
