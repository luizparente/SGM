import { Component, OnInit, Input } from '@angular/core';
import { IUser } from 'src/app/domain/user.model';
import { UserService } from 'src/app/modules/core/services/user/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  public user: any;
  public isCitizen: boolean = true;
  public isFieldDispatcher: boolean = false;
  public isFieldTechnician: boolean = false;

  constructor(private _userService: UserService,
              private _router: Router) {
    this._userService.onLogin.subscribe((user: IUser) => {
      this.user = user;
      this.updateRoles();
    });
  }

  ngOnInit(): void {
    this.updateRoles();
    this.user = this._userService.getUser();
  }

  public logOut(): void {
    this.user = undefined;
    this._userService.logout();
    this._router.navigate([""]);
  }

  private updateRoles(): void {    
    this.isCitizen = this._userService.isCurrentUserInRole('citizen');
    this.isFieldDispatcher = this._userService.isCurrentUserInRole('field-dispatcher');
    this.isFieldTechnician = this._userService.isCurrentUserInRole('field-technician');
  }
}
