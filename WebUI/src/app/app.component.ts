import { Component } from '@angular/core';
import { IUser } from './domain/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title: string = 'SGM';
  public user?: IUser;
}
