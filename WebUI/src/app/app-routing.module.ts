import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './modules/shared/components/home/home.component';
import { LoginComponent } from './modules/shared/components/login/login.component';
import { NotFoundComponent } from './modules/shared/components/not-found/not-found.component';
import { LoginGuard } from './modules/shared/guards/login/login.guard';
import { ServicesComponent } from './modules/field-manager/components/services/services.component';
import { RequestsComponent } from './modules/citizen/components/requests/requests.component';
import { MyServicesComponent } from './modules/field-technician/components/my-services/my-services.component';

const routes: Routes = [
  { path: "", redirectTo: "home", pathMatch: 'full' },
  { path: "home", component: HomeComponent },
  { path: "login", component: LoginComponent },
  { path: "requests", component: RequestsComponent, canActivate: [LoginGuard] },
  { path: "services", component: ServicesComponent, canActivate: [LoginGuard] },
  { path: "my-services", component: MyServicesComponent, canActivate: [LoginGuard] },
  { path: "**", component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
