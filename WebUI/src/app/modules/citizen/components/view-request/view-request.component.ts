import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IRequest } from 'src/app/domain/request.model';

@Component({
  selector: 'app-view-request',
  templateUrl: './view-request.component.html',
  styleUrls: ['./view-request.component.css']
})
export class ViewRequestComponent implements OnInit {
  @Input()
  public selectedRequest?: IRequest;
  @Output()
  public onClose: EventEmitter<void> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  public closeRequest(): void {
    this.onClose.emit();
  }
}
