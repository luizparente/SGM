import { Component, Output, EventEmitter } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { UserService } from 'src/app/modules/core/services/user/user.service';
import { IUser } from 'src/app/domain/user.model';

@Component({
  template: `
    <h5 class="text-center">Logar no sistema como:</h5>
    <mat-selection-list #users [multiple]="false">
      <mat-list-option value="citizen" (click)="login(users.selectedOptions.selected[0]?.value)">
        Cidadão
      </mat-list-option>
      <mat-list-option value="field-dispatcher" (click)="login(users.selectedOptions.selected[0]?.value)">
        Gerente de Campo
      </mat-list-option>
      <mat-list-option value="field-technician" (click)="login(users.selectedOptions.selected[0]?.value)">
        Tecnico de Campo
      </mat-list-option>
    </mat-selection-list>   
  `
})
export class BottomSheetLoginAs {
  @Output()
  public onLogin: EventEmitter<string> = new EventEmitter<string>();

  constructor(private _bottomSheetRef: MatBottomSheetRef<BottomSheetLoginAs>) {}

  login(user: string): void {
    this._bottomSheetRef.dismiss();
    this.onLogin.emit(user);
  }
}