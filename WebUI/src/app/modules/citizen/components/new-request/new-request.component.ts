import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IRequest } from 'src/app/domain/request.model';
import { IUser } from 'src/app/domain/user.model';
import { FormControl, Validators } from '@angular/forms';
import { UserService } from 'src/app/modules/core/services/user/user.service';
import { RequestService } from 'src/app/modules/core/services/request/request.service';

@Component({
  selector: 'app-new-request',
  templateUrl: './new-request.component.html',
  styleUrls: ['./new-request.component.css']
})
export class NewRequestComponent implements OnInit {
  public newRequestForm: FormControl = new FormControl('', [Validators.required]);
  public user: IUser | null = null;
  @Input()
  public newRequest?: IRequest;
  @Output()
  public onClose: EventEmitter<void> = new EventEmitter();
  @Output()
  public onCreate: EventEmitter<IRequest> = new EventEmitter<IRequest>();

  get isFormValid(): boolean {
    return this.newRequest != null && this.newRequest.header?.length > 1 && this.newRequest.body?.length > 1 && this.newRequest.category?.length > 1;
  }

  constructor(private _userService: UserService,
              private _requestService: RequestService) { }

  ngOnInit(): void {
    this.user = this._userService.getUser();
  }

  public discardRequestDraft(): void {
    this.onClose.emit();
  }

  public createRequest(request: IRequest): void {
    this._requestService.create(request);
    this.onCreate.emit();
    this.onClose.emit();
  }
}
