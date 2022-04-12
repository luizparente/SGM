import { Component, OnInit } from '@angular/core';
import { ServiceService } from 'src/app/modules/core/services/service/service.service';
import { IService } from 'src/app/domain/service.model';

@Component({
  selector: 'app-my-services',
  templateUrl: './my-services.component.html',
  styleUrls: ['./my-services.component.css']
})
export class MyServicesComponent implements OnInit {
  public services: IService[] = [];

  constructor(private _serviceService: ServiceService) { }

  ngOnInit(): void {
    this.services = this._serviceService.getAllPending();
  }
}
