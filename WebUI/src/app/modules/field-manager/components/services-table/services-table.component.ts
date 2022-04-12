import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { IService } from 'src/app/domain/service.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { ServiceService } from 'src/app/modules/core/services/service/service.service';

@Component({
  selector: 'app-services-table',
  templateUrl: './services-table.component.html',
  styleUrls: ['./services-table.component.css']
})
export class ServicesTableComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;
  @ViewChild(MatTable) servicesTable?:  MatTable<IService>;
  @Output()
  private onSelectService: EventEmitter<IService> = new EventEmitter<IService>();
  @Input()
  public services?: IService[];
  public data: MatTableDataSource<IService>;
  public displayedColumns: string[] = ['serviceGuid', 'title', 'assignee', 'scheduled', 'completed'];

  constructor(private _serviceService: ServiceService) { 
    this.data = new MatTableDataSource<IService>(this.services);
  }

  ngOnInit(): void {
    this.data = new MatTableDataSource<IService>(this.services);
  }

  ngAfterViewInit(): void {
    if (this.paginator) {
      this.data.paginator = this.paginator;
    }
  }

  public selectService(service: IService): void {
    this.onSelectService.emit(service);
  }
}
