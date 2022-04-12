import { Injectable } from '@angular/core';
import { IRequest } from 'src/app/domain/request.model';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private requestCreation: Subject<IRequest[]> = new Subject<IRequest[]>();
  public onRequestCreated: Observable<IRequest[]> = this.requestCreation.asObservable();

  private requests: IRequest[] =  [
    { requestGuid: "C57352", author: { username: 'Cidadão', roles: []}, category: "Iluminação Pública", address: 'Rua do Teste, 123', header: "Rua escura", body: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", dateOpened: new Date() },
    { requestGuid: "C78355", author: { username: 'Cidadão', roles: []}, category: "Lorem Ipsum Dolor", address: 'Rua Lorem Ipsum, 10', header: "Lorem ipsum dolor sit amet", body: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", dateOpened: new Date() },
    { requestGuid: "C61948", author: { username: 'Cidadão', roles: []}, category: "Lorem Ipsum Dolor", address: 'Rua Lorem Ipsum, 20', header: "Lorem ipsum dolor sit amet", body: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", response: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", dateOpened: new Date(), dateClosed: new Date() },
    { requestGuid: "C95129", author: { username: 'Cidadão', roles: []}, category: "Lorem Ipsum Dolor", address: 'Rua Lorem Ipsum, 30', header: "Lorem ipsum dolor sit amet", body: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", response: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", dateOpened: new Date(), dateClosed: new Date() },
    { requestGuid: "C18564", author: { username: 'Cidadão', roles: []}, category: "Lorem Ipsum Dolor", address: 'Rua Lorem Ipsum, 40', header: "Lorem ipsum dolor sit amet", body: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", response: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", dateOpened: new Date(), dateClosed: new Date() },
    { requestGuid: "C94853", author: { username: 'Cidadão', roles: []}, category: "Lorem Ipsum Dolor", address: 'Rua Lorem Ipsum, 50', header: "Lorem ipsum dolor sit amet", body: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", response: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", dateOpened: new Date(), dateClosed: new Date() },
  ];

  constructor() { }

  public getAll(): IRequest[] {
    return this.requests;
  }

  public get(requestGuid: string): IRequest | null {
    let matches = this.requests.filter(r => r.requestGuid == requestGuid);

    if (matches.length > 0) {
      return matches[0];
    }
    
    return null;
  }

  public create(request: IRequest): void {
    this.requests.unshift(request);
    this.requestCreation.next(this.requests);
  }

  public update(request: IRequest): void {
    this.delete(request);
    this.create(request);
  }

  public delete(request: IRequest): void {
    this.requests = this.requests.filter(r => r.requestGuid != request.requestGuid);
  }
}
