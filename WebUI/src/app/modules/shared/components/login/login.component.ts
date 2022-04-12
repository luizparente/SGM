import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { BottomSheetLoginAs } from '../bottom-sheet-login-as/bottom-sheet-login-as.component';
import { IUser } from 'src/app/domain/user.model';
import { Router } from '@angular/router';
import { UserService } from 'src/app/modules/core/services/user/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public username?: string;
  public password?: string;
  
  public loginForm = new FormControl('', [
    Validators.required
  ]);

  constructor(private _bottomSheet: MatBottomSheet,
              private _userService: UserService,
              private _router: Router) { }

  ngOnInit(): void {
  }

  public onSubmit(): void {
    let bsRef = this._bottomSheet.open(BottomSheetLoginAs);
    bsRef.instance.onLogin.subscribe((user: string) => {
      this._userService.authenticate(user, '').subscribe((result: IUser) => {
        if (result) {
          console.log(`Logged in as:`);
          console.log(result);

          this._router.navigate(['']);
        }
      });
    });
  }
}

