import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IService } from 'src/app/domain/service.model';

@Component({
  selector: 'app-view-service',
  templateUrl: './view-service.component.html',
  styleUrls: ['./view-service.component.css']
})
export class ViewServiceComponent implements OnInit {
  @Input()
  public service?: IService;
  @Output()
  public onClose: EventEmitter<void> = new EventEmitter();


  constructor() { }

  ngOnInit(): void {
  }

  public closeService(): void {
    this.onClose.emit();
  }
}
