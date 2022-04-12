import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './modules/shared/shared.module';
import { CitizenModule } from './modules/citizen/citizen.module';
import { LoginGuard } from './modules/shared/guards/login/login.guard';
import { FieldManagerModule } from './modules/field-manager/field-manager.module';
import { FieldTechnicianModule } from './modules/field-technician/field-technician.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    CitizenModule,
    FieldManagerModule,
    FieldTechnicianModule
  ],
  providers: [LoginGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
