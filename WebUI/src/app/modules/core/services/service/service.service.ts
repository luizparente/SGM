import { Injectable } from '@angular/core';
import { RequestService } from '../request/request.service';
import { IRequest } from 'src/app/domain/request.model';
import { IService } from 'src/app/domain/service.model';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  private requests?: IRequest[];
  private services: IService[] = [];
  private requestUpdate: Subject<IService[]> = new Subject<IService[]>();
  public onServiceUpdated: Observable<IService[]> = this.requestUpdate.asObservable();

  constructor(private _requestService: RequestService) { 
    this.requests = this._requestService.getAll();
    this.initServices();
  }

  public getAll(): IService[] {
    return this.services;
  }

  public getAllPending(): IService[] {
    return this.services.filter(s => !s.completed && s.scheduled);

    // LP: Stub
    // let stub = this.services.map((service: IService) => <IService> {
    //   serviceGuid: service.serviceGuid,
    //   title: service.title,
    //   request: service.request,
    //   assignee: { 
    //     technicianGuid: 'T12345',
    //     user: {
    //       username: 'Tecnico de Campo',
    //       roles: [{
    //           name: 'field-tecnician'
    //         } 
    //       ]
    //     },
    //   },
    //   address: 'Rua do Teste, 123',
    //   scheduled: new Date()
    // });

    // return stub;
  }

  public update(service: IService): void {
    this.services = this.services.filter(s => s.serviceGuid != service.serviceGuid);
    this.services.unshift(service);

    if (service.completed || service.response) {
      service.request.dateClosed = service.completed;
      service.request.response = service.response;

      this._requestService.update(service.request);
    }

    this.requestUpdate.next(this.services);
  }

  private initServices(): void {
    let scheduled = this.requests;

    scheduled?.forEach((request: IRequest) => {
      let service: IService = { 
        serviceGuid: 'S' + Math.floor(Math.random() * 100000).toString(), 
        title: request.header,
        request: request,        
        address: 'Rua do Teste, 123'
      };

      if (request.dateClosed) {
        service.completed = new Date();
        service.scheduled = new Date();
        service.response = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
        service.assignee = { 
          technicianGuid: 'T12345',
          user: {
            username: 'Tecnico de Campo',
            roles: [{
                name: 'field-tecnician'
              } 
            ]
          },
        };
      }

      this.services?.push(service);
    });
  }
}
