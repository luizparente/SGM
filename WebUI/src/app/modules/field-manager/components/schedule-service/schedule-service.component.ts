import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IService } from 'src/app/domain/service.model';
import { ServiceService } from 'src/app/modules/core/services/service/service.service';
import { ITechnician } from 'src/app/domain/technician.model';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-schedule-service',
  templateUrl: './schedule-service.component.html',
  styleUrls: ['./schedule-service.component.css']
})
export class ScheduleServiceComponent implements OnInit {
  private serv?: IService;
  public assignee?: ITechnician;
  public date?: Date;
  public technicians: ITechnician[] = [
    { 
      technicianGuid: 'T12345',
      user: {
        username: 'Tecnico de Campo 1',
        roles: [{
            name: 'field-tecnician'
          } 
        ]
      }
    },
    { 
      technicianGuid: 'T23456',
      user: {
        username: 'Tecnico de Campo 2',
        roles: [{
            name: 'field-tecnician'
          } 
        ]
      }
    },
    { 
      technicianGuid: 'T34567',
      user: {
        username: 'Tecnico de Campo 3',
        roles: [{
            name: 'field-tecnician'
          } 
        ]
      }
    },
  ];

  @Input()
  public set service(service: IService | undefined) {
    this.serv = service;
    this.date = service?.scheduled;
    this.assignee = service?.assignee;
  }
  public get service(): IService | undefined { 
    return this.serv; 
  }
  @Output()
  public onClose: EventEmitter<void> = new EventEmitter();
  @Output()
  public onScheduled: EventEmitter<void> = new EventEmitter();

  public get canSchedule(): boolean {
    return this.assignee != null && this.assignee != undefined && this.date != null && this.date != undefined;
  }

  constructor(private _serviceService: ServiceService) { }

  ngOnInit(): void {
  }

  public closeService(): void {
    this.onClose.emit();
  }

  public update(service: IService): void {
    service.scheduled = new Date();
    service.assignee = this.assignee;
    service.scheduled = this.date;

    this._serviceService.update(service);

    this.assignee = undefined;
    this.date = undefined;

    this.onClose.emit();
    this.onScheduled.emit();
  }

  public dateFilter (d: Date | null): boolean {
    const now = new Date();
    const day = (d || new Date()).getDay();

    if (d) {
      return day !== 0 && day !== 6 && d > now;
    }

    return false;
  }

  public technicianComparison(option: ITechnician, value: ITechnician): boolean {
    if (option && value) {
      return option.technicianGuid === value.technicianGuid;
    }

    return false;
  }
}
