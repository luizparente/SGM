import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IRequest } from 'src/app/domain/request.model';
import { MatDrawer } from '@angular/material/sidenav';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from 'src/app/modules/core/services/user/user.service';
import { RequestService } from 'src/app/modules/core/services/request/request.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {
  public user: any;
  public requests: Array<IRequest> = [];
  public open: Array<IRequest> = [];
  public closed: Array<IRequest> = [];
  public selectedRequest?: IRequest;
  public newRequest?: IRequest;
  @ViewChild('drawer')
  private drawer?: MatDrawer;

  constructor(private _userService: UserService,
              private _requestService: RequestService,
              private _snackBar: MatSnackBar,
              private _router: Router) { 
    this._requestService.onRequestCreated.subscribe((requests: IRequest[]) => {
      this.requests = requests;
      this.closed = this.requests.filter(r => r.dateClosed);
      this.open = this.requests.filter(r => !r.dateClosed);
    });
  }

  ngOnInit(): void {
    this.user = this._userService.getUser();
    this.requests = this._requestService.getAll();
    this.closed = this.requests.filter(r => r.dateClosed);
    this.open = this.requests.filter(r => !r.dateClosed);
  }

  public displayRequest(request: IRequest): void {
    this.newRequest = undefined;

    if (this.drawer?.opened) {
      this.drawer?.close().then(() => {
        this.selectedRequest = request;
        this.drawer?.open();
      });

      return;
    }

    this.selectedRequest = request;
    this.drawer?.open();
  }

  public closeRequest(): void {
    this.drawer?.close();
    this.selectedRequest = undefined;
    this.newRequest = undefined;
  }

  public createRequest(): void {
    if (this.drawer?.opened) {
      this.drawer?.close().then(() => {
        if (this.selectedRequest) {
          this.selectedRequest = undefined;
          this.newRequest = this.newEmptyRequest();
          this.drawer?.open();
        }
        else {
          this.newRequest = undefined;
          this.selectedRequest = undefined;
        }
      });

      return;
    }

    this.selectedRequest = undefined;
    this.newRequest = this.newEmptyRequest();
    this.drawer?.toggle();
  }

  public discardRequestDraft(): void {
    this.newRequest = undefined;
    this.selectedRequest = undefined
    this.drawer?.close();
  }

   public displayRequestCreatedConfirmation(): void {
    this._snackBar.open('Chamado criado com sucesso!', 'OK', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
   }

  private newEmptyRequest(): IRequest {
    return { requestGuid: 'C' + Math.floor(Math.random() * 100000).toString(), author: this.user, address: "", category: "", header: "", body: "", dateOpened: new Date() };
  }
}
