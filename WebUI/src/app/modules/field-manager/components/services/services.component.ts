import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ServiceService } from 'src/app/modules/core/services/service/service.service';
import { IService } from 'src/app/domain/service.model';
import { MatDrawer } from '@angular/material/sidenav';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrls: ['./services.component.css']
})
export class ServicesComponent implements OnInit {
  public selectedService?: IService;
  public open?: IService[];
  public history?: IService[];
  @Input()
  public services: IService[] = [];
  @ViewChild('drawer')
  private drawer?: MatDrawer;

  constructor(private _serviceService: ServiceService,
              private _snackBar: MatSnackBar) { 
    this._serviceService.onServiceUpdated.subscribe((services: IService[]) => {
      this.services = services;
      this.open = this.services.filter(s => !s.completed);
      this.history = this.services.filter(s => s.completed);
    });
  }

  ngOnInit(): void {
    this.services = this._serviceService.getAll();
    this.open = this.services.filter(s => !s.completed);
    this.history = this.services.filter(s => s.completed);
  }

  public displayService(service?: IService): void {
    if (this.drawer?.opened) {
      this.drawer?.close().then(() => {
        this.selectedService = service;
        this.drawer?.open();
      });

      return;
    }

    this.selectedService = service;
    this.drawer?.open();
  }

  public closeService(): void {
    this.drawer?.close();
    this.selectedService = undefined;
  }

  public displayServiceScheduledConfirmation(): void {
   this._snackBar.open('Serviço agendado com sucesso!', 'OK', {
     duration: 5000,
     horizontalPosition: 'center',
     verticalPosition: 'top',
   });
  }
}
