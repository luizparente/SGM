import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MapInfoWindow, MapMarker } from '@angular/google-maps';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { IService } from 'src/app/domain/service.model';
import { MatDrawer } from '@angular/material/sidenav';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-service-map',
  templateUrl: './service-map.component.html',
  styleUrls: ['./service-map.component.css']
})
export class ServiceMapComponent implements OnInit {
  private rawServices: IService[] = [];
  public mapData: { service: IService, markerOptions: google.maps.MarkerOptions }[] = [];
  public selectedService?: IService;
  @ViewChild('drawer')
  private drawer?: MatDrawer;

  @Input()
  public set services(services: IService[]) {
    this.updateMapData(services);
  };

  @ViewChild(MapInfoWindow) infoWindow?: MapInfoWindow;
  public apiLoaded?: Observable<boolean>;
  public mapOptions: any = {
    center: {
      lat: -23.556837, 
      lng: -46.6651571},
    zoom: 11,
    styles: [{
        featureType: 'poi',
        elementType: 'labels',
        stylers: [{
            visibility: 'off'
          }
        ]
      }
    ]
  };  

  constructor(private _http: HttpClient,
              private _snackBar: MatSnackBar) { 
    try {
      this.apiLoaded = _http.jsonp('https://maps.googleapis.com/maps/api/js?key=AIzaSyBGKJ83EnNSIox7_grDRBUoJr4IVD1X_Uo', 'callback')
      .pipe(
        map(() => true),
        catchError(() => of(false)),
      );
    }
    catch {
      // Swallow exception.
    }
  }

  ngOnInit(): void {
  }
  
  public selectService(serviceEntry: { service: IService, markerOptions: google.maps.MarkerOptions }, marker: MapMarker): void {
    if (this.drawer?.opened) {
      this.drawer?.close();
    }

    this.selectedService = serviceEntry.service;
    this.infoWindow?.open(marker);
  }

  private updateMapData(services: IService[]) {
    this.rawServices = services;

    this.rawServices.forEach(service => {
      let item = {
        service: service,
        markerOptions: {
          position: this.getRandomPosition(),
          draggable: false,
          title: `Serviço #${service.serviceGuid}`
        }
      };

      this.mapData.push(item);
    });
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

  public displayServiceCompletedConfirmation(): void {
    this.closeService();
    this.selectedService = undefined;

    this._snackBar.open('Serviço finalizado com sucesso!', 'OK', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }

  private getRandomPosition(): { lat: number, lng: number } {
    let random = Math.random() * Math.random() / 10;

    if (Math.random() < 0.5) {
      random += random - Math.random() / 1000;
    }
    else {
      random -= Math.random() / 1000;
    }

    return { 
      lat: -23.55458679268766 + random, 
      lng: -46.65628435257874 + random 
    };
  }
}
