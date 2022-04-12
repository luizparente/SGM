import { Component, OnInit, Input, AfterViewInit, ViewChild, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { IRequest } from 'src/app/domain/request.model';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { IUser } from 'src/app/domain/user.model';
import { RequestService } from 'src/app/modules/core/services/request/request.service';

@Component({
  selector: 'app-requests-table',
  templateUrl: './requests-table.component.html',
  styleUrls: ['./requests-table.component.css']
})
export class RequestsTableComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator | null = null;
  @ViewChild(MatTable) requestsTable?:  MatTable<IRequest>;
  @Input()
  public requests: Array<IRequest> = [];
  public data: MatTableDataSource<IRequest>;
  public displayedColumns: string[] = ['requestGuid', 'category', 'header', 'dateOpened', 'dateClosed'];
  @Output()
  private onSelectRequest: EventEmitter<IRequest> = new EventEmitter<IRequest>();

  public user?: IUser;

  constructor(private _requestService: RequestService) { 
    this.data = new MatTableDataSource<IRequest>(this.requests);
    
    this._requestService.onRequestCreated.subscribe((requests: IRequest[]) => {      
      this.data = new MatTableDataSource<IRequest>(requests);
      this.requestsTable?.renderRows();
      this.data.paginator = this.paginator;
    });
  }

  ngOnInit(): void {
    this.data = new MatTableDataSource<IRequest>(this.requests);
  }

  ngAfterViewInit(): void {
    if (this.paginator) {
      this.data.paginator = this.paginator;
    }
  }

  public selectRequest(request: IRequest): void {
    this.onSelectRequest.emit(request);
  }
}