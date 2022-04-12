import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IService } from 'src/app/domain/service.model';
import { ServiceService } from 'src/app/modules/core/services/service/service.service';

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
  @Output()
  public onCompleted: EventEmitter<void> = new EventEmitter();

  constructor(private _serviceService: ServiceService) { }

  ngOnInit(): void {
  }

  public closeService(): void {
    this.onClose.emit();
  }

  public completeService(service: IService): void {
    service.completed = new Date();
    this._serviceService.update(service);
    this.service = undefined;

    this.onCompleted.emit();
  }
}
